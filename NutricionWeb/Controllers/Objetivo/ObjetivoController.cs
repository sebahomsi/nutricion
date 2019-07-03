using NutricionWeb.Models.Objetivo;
using Servicio.Interface.Objetivo;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Objetivo
{
    public class ObjetivoController : Controller
    {
        private readonly IObjetivoServicio _objetivoServicio;

        public ObjetivoController(IObjetivoServicio objetivoServicio)
        {
            _objetivoServicio = objetivoServicio;
        }

        // POST: Objetivo/Create
        [HttpPost]
        public async Task<ActionResult> Create(ObjetivoViewModel vm)
        {
            try
            {
                var obj = await _objetivoServicio.GetByPacienteId(vm.PacienteId);
                var idReal = obj.Id;
                if (idReal == 0)
                {
                    // nuevo
                    var dto = new ObjetivoDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _objetivoServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new ObjetivoDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = idReal
                    };

                    await _objetivoServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new {Id = vm.PacienteId});
            }
            catch (Exception e)
            {
                return null;
            }
        }

    }
}
