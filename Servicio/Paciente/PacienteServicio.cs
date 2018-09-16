using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Paciente;

namespace Servicio.Paciente
{
    public class PacienteServicio : ServicioBase, IPacienteServicio
    {
        public async Task<long> Add(PacienteDto dto)
        {
            var paciente = new Dominio.Entidades.Paciente()
            {
                Codigo = dto.Codigo,
                Apellido = dto.Apellido,
                Nombre = dto.Nombre,
                Celular = dto.Celular,
                Dni = dto.Dni,
                Direccion = dto.Direccion,
                Mail = dto.Mail,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                FechaNac = dto.FechaNac,
                Foto = dto.Foto,
                Eliminado = false,
                Estado = dto.Estado,
                TieneAnalitico = false
            };

            Context.Personas.Add(paciente);
            await Context.SaveChangesAsync();

            return paciente.Id;
        }

        public async Task Update(PacienteDto dto)
        {
            var paciente = Context.Personas.OfType<Dominio.Entidades.Paciente>().FirstOrDefault(x => x.Id == dto.Id);
            if(paciente == null) throw new ArgumentNullException();

            paciente.Apellido = dto.Apellido;
            paciente.Nombre = dto.Nombre;
            paciente.Celular = dto.Celular;
            paciente.Dni = dto.Dni;
            paciente.Direccion = dto.Direccion;
            paciente.Mail = dto.Mail;
            paciente.Telefono = dto.Telefono;
            paciente.Sexo = dto.Sexo;
            paciente.FechaNac = dto.FechaNac;
            paciente.Foto = dto.Foto;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<PacienteDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<PacienteDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextCode()
        {
            throw new NotImplementedException();
        }
    }
}
