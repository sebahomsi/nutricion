using NutricionWeb.Models.Anamnesis;
using Servicio.Interface.Anamnesis;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Anamnesis
{
    public class AnamnesisController : Controller
    {
        private readonly IAnamnesisServicio _anamnesisServicio;

        public AnamnesisController(IAnamnesisServicio anamnesisServicio)
        {
            _anamnesisServicio = anamnesisServicio;
        }

        // POST: Anamnesis/Create
        [HttpPost]
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Create(AnamnesisViewModel vm)
        {
            try
            {
                var anamnesis = await _anamnesisServicio.GetByPacienteId(vm.PacienteId);
                var idReal = anamnesis.Id;
                if (idReal == 0)
                {
                    // nuevo
                    var dto = new AnamnesisDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _anamnesisServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new AnamnesisDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = idReal
                    };

                    await _anamnesisServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new { Id = vm.PacienteId });
            }
            catch(Exception e)
            {
                return null;
            }
        }

    }
}
