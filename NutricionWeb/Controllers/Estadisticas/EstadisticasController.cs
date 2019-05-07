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
            var datos = await _datoAntropometricoServicio.GetByIdPaciente(id.Value);

            var fechas = "";
            var pesos = "";
            var backColor = "";
            var borderColor = "";

            foreach (var dato in datos)
            {
                fechas += ',' + dato.FechaMedicion.ToShortDateString();
                pesos += ',' + dato.PesoActual;
                backColor += ',' + "rgba(255, 0, 0, 0.2)";
                borderColor += ',' + "rgba(0, 255, 0, 1)";
            }


            ViewBag.Pesos = pesos.Remove(0, 1);
            ViewBag.Fechas = fechas.Remove(0, 1);
            ViewBag.BackColor = backColor.Remove(0, 1);
            ViewBag.BorderColor = borderColor.Remove(0, 1);

            return PartialView();
        }
    }
}