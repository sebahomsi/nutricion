using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Servicio.Interface.DatoAntropometrico;

namespace NutricionWeb.Controllers.Estadisticas
{
    public class EstadisticasController : Controller
    {
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;

        public EstadisticasController(IDatoAntropometricoServicio datoAntropometricoServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
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

    }
}