using NutricionWeb.Models.Estrategia;
using Servicio.Interface.Estrategia;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Estrategia
{
    public class EstrategiaController : Controller
    {

        private readonly IEstrategiaServicio _estrategiaServicio;

        public EstrategiaController(IEstrategiaServicio estrategiaServicio)
        {
            _estrategiaServicio = estrategiaServicio;
        }

        // POST: Estrategia/Create
        [HttpPost]
        public async Task<ActionResult> Create(EstrategiaViewModel vm)
        {
            try
            {
                var estrategia = await _estrategiaServicio.GetByPacienteId(vm.PacienteId);
                var idReal = estrategia.Id;
                if (idReal == 0)
                {
                    // nuevo
                    var dto = new EstrategiaDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _estrategiaServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new EstrategiaDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = idReal
                    };

                    await _estrategiaServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new { Id = vm.PacienteId });
            }
            catch
            {
                return View();
            }
        }
    }
}
