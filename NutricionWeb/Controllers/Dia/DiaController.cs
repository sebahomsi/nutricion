using NutricionWeb.Models.Comida;
using NutricionWeb.Models.Dia;
using Servicio.Interface.Dia;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Dia
{
    [Authorize(Roles = "Administrador")]
    public class DiaController : Controller
    {
        private readonly IDiaServicio _diaServicio;

        public DiaController(IDiaServicio diaServicio)
        {
            _diaServicio = diaServicio;
        }

        // GET: Dia/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dia = await _diaServicio.GetById(id.Value);

            return View(new DiaViewModel()
            {
                Id = dia.Id,
                Codigo = dia.Codigo,
                Descripcion = dia.Descripcion,
                PlanAlimenticioId = dia.PlanAlimenticioId,
                PlanAlimenticioStr = dia.PlanAlimenticioStr,
                Comidas = dia.Comidas.Select(x=> new ComidaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    DiaId = x.DiaId,
                    DiaStr = x.DiaStr
                }).ToList()
            });
        }
    }
}
