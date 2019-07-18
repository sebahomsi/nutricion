using System;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Estado;
using Servicio.Interface.Turno;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dominio.Entidades.MetaData;
using NutricionWeb.Models.Pago;
using Servicio.Interface.Paciente;
using Servicio.Interface.Pago;
using Servicio.Pago;

namespace NutricionWeb.Controllers.Estadisticas
{
    public class EstadisticasController : Controller
    {
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;
        private readonly ITurnoServicio _turnoServicio;
        private readonly IEstadoServicio _estadoServicio;
        private readonly IPagoServicio _pagoServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public EstadisticasController(IDatoAntropometricoServicio datoAntropometricoServicio, ITurnoServicio turnoServicio, IEstadoServicio estadoServicio, IPagoServicio pagoServicio, IPacienteServicio pacienteServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _turnoServicio = turnoServicio;
            _estadoServicio = estadoServicio;
            _pagoServicio = pagoServicio;
            _pacienteServicio = pacienteServicio;   
        }

        public async Task<ActionResult> EstadisticasGenerales()
        {
            return View();
        }
        public async Task<ActionResult> EstadisticasPagos(DateTime fechaPago)
        {
            var fechaIni = fechaPago;

            var fechaFin = fechaPago.AddMonths(1);
            var pagos = await _pagoServicio.GetByDate(fechaIni, fechaFin, false, "");

            var vms = pagos.Select(x => new PagoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteStr = x.PacienteStr,
                PacienteId = x.PacienteId,
                Fecha = x.Fecha,
                Concepto = x.Concepto,
                EstaEliminado = x.EstaEliminado,
                Monto = x.Monto,
            });
            return Json(vms, JsonRequestBehavior.AllowGet);
        }

        // GET: Estadisticas
        public async Task<ActionResult> Index(long? id)
        { 
            ViewBag.PacienteId = id.Value;
            
            return PartialView();
        }
        public async Task<ActionResult> ObtenerDatosAntropometricos(long? pacienteId)
        {
            var datos = await _datoAntropometricoServicio.GetByIdPaciente(pacienteId.Value);

            datos = datos.Where(x => !x.Eliminado);

            var json = datos.OrderBy(x=>x.FechaMedicion).Select(x => new
            {
                Fecha = x.FechaMedicion.ToShortDateString(),
                Peso = decimal.Parse(x.PesoActual),
                BackColor = "rgba(255, 0, 0, 0.2)",
                BorderColor = "rgba(0, 255, 0, 1)",
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ObtenerPagos(string fechaDesde, string fechaHasta)
        {
            var desde = DateTime.Parse(fechaDesde);
            var hasta = DateTime.Parse(fechaHasta);
            var datos = await _pagoServicio.GetByDate(desde, hasta, false, "");
            var fechas = new List<DateTime>();
            var auxs= new List<PagoAux>();

            foreach (var pago in datos)
            {
                if (!fechas.Contains(pago.Fecha))
                {
                    fechas.Add(pago.Fecha);
                }
            }

            foreach (var fecha in fechas)
            {
                var pago=new PagoAux()
                {
                    Fecha = fecha,
                    Monto = datos.Where(x=>x.Fecha==fecha).Sum(x=>x.Monto)
                };
                auxs.Add(pago);
            }

            var json = auxs.OrderBy(x => x.Fecha).Select(x=>new
            {
                Fecha = x.Fecha.ToShortDateString(),
                Monto = decimal.Parse(x.Monto.ToString()),
                BackColor = "rgba(75, 192, 192, 1)",
                BorderColor = "rgba(75, 192, 192, 1)",
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ObtenerPacientes(string fechaDesde, string fechaHasta)
        {
            var auxs = new List<PacienteAux>();
            var desde = DateTime.Parse(fechaDesde);
            var hasta = DateTime.Parse(fechaHasta);

            var pacientesNuevos = await _pacienteServicio.GetByDateNewPacientes(desde, hasta);
            var pacientesActivos = await _pacienteServicio.GetByDateActivePacientes(desde, hasta);
            var pacientesInactivos = await _pacienteServicio.GetByDateInactivePacientes(desde, hasta);

            var nuevos = new PacienteAux()
            {
                Cantidad = pacientesNuevos.Count(),
                Estado = "Nuevo"
            };
            var activos = new PacienteAux()
            {
                Cantidad = pacientesActivos.Count(),
                Estado = "Con Medición"
            };
            var inactivos = new PacienteAux()
            {
                Cantidad = pacientesInactivos.Count(),
                Estado = "Sin Medición"
            };
            auxs.Add(nuevos);
            auxs.Add(activos);
            auxs.Add(inactivos);

            var json = auxs.OrderBy(x => x.Estado).Select(x => new
            {
                Cantidad = x.Cantidad,
                Estado = x.Estado,
                BackColor = "rgba(75, 192, 192, 1)",
                BorderColor = "rgba(75, 192, 192, 1)",
            });

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> ObtenerTurnosPorPaciente(long? pacienteId)
        {
            var datos = await _turnoServicio.GetByIdPaciente(pacienteId.Value);

            var estados = await _estadoServicio.Get(false,"");

            var datosOrdenados = new List<Contador>();

            foreach (var estado in estados)
            {
                var cont = new Contador()
                {
                    Color = ConvertirColor(estado.Color),
                    Descripcion = estado.Descripcion,
                    Cantidad = 0
                };
                foreach (var turno in datos)
                {
                    if (turno.EstadoId == estado.Id)
                    {
                        cont.Cantidad++;
                    }
                }
                datosOrdenados.Add(cont);
            }
            var json = datosOrdenados;
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        private string ConvertirColor(string color)
        {
            color = color.Replace("#", "");
            var r = int.Parse(color.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            var g = int.Parse(color.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            var b = int.Parse(color.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

            var dev = $"rgba({r},{g},{b},1)";
            return dev;        

        }

        private class Contador
        {
            public string Descripcion { get; set; }

            public string Color { get; set; }

            public int Cantidad { get; set; }
        }

        private class PagoAux
        {
            public DateTime Fecha { get; set; }

            public double Monto { get; set; }

        }

        private class PacienteAux
        {
            public string Estado { get; set; }

            public int Cantidad { get; set; }
        }
    }
    
}