using System;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Estado;
using Servicio.Interface.Turno;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Models.Pago;
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

        public EstadisticasController(IDatoAntropometricoServicio datoAntropometricoServicio, ITurnoServicio turnoServicio, IEstadoServicio estadoServicio, IPagoServicio pagoServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _turnoServicio = turnoServicio;
            _estadoServicio = estadoServicio;
            _pagoServicio = pagoServicio;
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


            var json = datos.OrderBy(x=>x.FechaMedicion).Select(x => new
            {
                Fecha = x.FechaMedicion.ToShortDateString(),
                Peso = decimal.Parse(x.PesoActual),
                BackColor = "rgba(255, 0, 0, 0.2)",
                BorderColor = "rgba(0, 255, 0, 1)",
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
    }
    
}