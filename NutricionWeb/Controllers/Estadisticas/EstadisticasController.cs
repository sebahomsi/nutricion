using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Estadisticas
{
    public class EstadisticasController : Controller
    {
        // GET: Estadisticas
        public async Task<ActionResult> Index(long? id)
        {
            return PartialView();
        }
    }
}