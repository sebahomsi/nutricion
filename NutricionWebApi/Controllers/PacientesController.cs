using AutoMapper;
using Servicio.Interface.Paciente;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using NutricionWebApi.Models.Paciente;

namespace NutricionWebApi.Controllers
{
    public class PacientesController : ApiController
    {

        private readonly IPacienteServicio _pacienteServicio;

        public PacientesController(IPacienteServicio pacienteServicio)
        {
            _pacienteServicio = pacienteServicio;
        }

        public async Task<IHttpActionResult> Get()
        {
            var pacientes = await _pacienteServicio.Get(null, false, string.Empty);

            
            return Ok(pacientes.Select(x => new PacienteWebApiViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToList());

        }

        public async Task<IHttpActionResult> Get(long id)
        {
            var paciente = await _pacienteServicio.GetById(id);

            if (paciente == null)  return NotFound();

            var pacienteResponse = Mapper.Map<PacienteWebApiViewModel>(paciente);

            return Ok(pacienteResponse);
        }
    }
}
