using Servicio.Interface.Empleado;
using Servicio.Interface.Usuario;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicio.Empleado
{
    public class EmpleadoServicio : ServicioBase, IEmpleadoServicio
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public EmpleadoServicio(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }

        public async Task<long> Add(EmpleadoDto dto)
        {
            var (id, mensaje) = await VerifyDuplicity(dto);

            if (id.HasValue) throw new ArgumentException(mensaje);

            var empleado = new Dominio.Entidades.Empleado()
            {
                Legajo = dto.Legajo,
                Apellido = dto.Apellido,
                Nombre = dto.Nombre,
                Dni = dto.Dni,
                Celular = dto.Celular,
                Cuit = dto.Cuit,
                Eliminado = false,
                FechaNac = dto.FechaNac,
                Foto = dto.Foto,
                Mail = dto.Mail,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                EstablecimientoId = dto.EstablecimientoId
            };

            Context.Personas.Add(empleado);          
            await Context.SaveChangesAsync();
            await _usuarioServicio.Crear(empleado.Mail, empleado.Dni,"Empleado" , empleado.EstablecimientoId);
            return empleado.Id;
        }

        public async Task Update(EmpleadoDto dto)
        {
            var empleado = await Context.Personas.OfType<Dominio.Entidades.Empleado>().FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (empleado == null) throw new ArgumentNullException();

            if (empleado.Mail != dto.Mail)
            {
                var (id, mensaje) = await VerifyDuplicity(dto);

                if (id.HasValue) throw new ArgumentException(mensaje);
            }

            empleado.Apellido = dto.Apellido;
            empleado.Nombre = dto.Nombre;
            empleado.Dni = dto.Dni;
            empleado.Celular = dto.Celular;
            empleado.Cuit = dto.Cuit;
            empleado.FechaNac = dto.FechaNac;
            empleado.Foto = dto.Foto;
            empleado.Mail = dto.Mail;
            empleado.Telefono = dto.Telefono;
            empleado.Sexo = dto.Sexo;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var empleado = await Context.Personas.OfType<Dominio.Entidades.Empleado>().FirstOrDefaultAsync(x => x.Id == id);

            if (empleado == null) throw new ArgumentNullException(); //buscar en google parametros validos

            empleado.Eliminado = !empleado.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<EmpleadoDto>> Get(long? establecimientoId,bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Empleado, bool>> expression = 
                x => x.Eliminado == eliminado && (x.Apellido.Contains(cadenaBuscar)
                || x.Nombre.Contains(cadenaBuscar))
                &&(!establecimientoId.HasValue || x.EstablecimientoId == establecimientoId);
            return await Context.Personas.OfType<Dominio.Entidades.Empleado>()
                .AsNoTracking()
                .Where(expression)
                .Select(x => new EmpleadoDto()
                {
                    Id = x.Id,
                    Legajo = x.Legajo,
                    Apellido = x.Apellido,
                    Nombre = x.Nombre,
                    Dni = x.Dni,
                    Celular = x.Celular,
                    Cuit = x.Cuit,
                    Eliminado = x.Eliminado,
                    FechaNac = x.FechaNac,
                    Foto = x.Foto,
                    Mail = x.Mail,
                    Telefono = x.Telefono,
                    Sexo = x.Sexo
                }).ToListAsync();
        }

        public async Task<EmpleadoDto> GetById(long id)
        {
            var empleado = await Context.Personas.OfType<Dominio.Entidades.Empleado>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (empleado == null) throw new ArgumentNullException();

            return new EmpleadoDto()
            {
                Id = empleado.Id,
                Legajo = empleado.Legajo,
                Apellido = empleado.Apellido,
                Nombre = empleado.Nombre,
                Dni = empleado.Dni,
                Celular = empleado.Celular,
                Cuit = empleado.Cuit,
                Eliminado = empleado.Eliminado,
                FechaNac = empleado.FechaNac,
                Foto = empleado.Foto,
                Mail = empleado.Mail,
                Telefono = empleado.Telefono,
                Sexo = empleado.Sexo
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Personas.OfType<Dominio.Entidades.Empleado>().AnyAsync()
                ? await Context.Personas.OfType<Dominio.Entidades.Empleado>().MaxAsync(x => x.Legajo) + 1
                : 1;
        }

        public async Task<(long? Id, string Mensaje)> VerifyDuplicity(EmpleadoDto dto)
        {
            var paciente = await Context.Personas
                .FirstOrDefaultAsync(x => x.Mail == dto.Mail);

            if (paciente != null) { return (paciente.Id, Mensaje: "Ya existe un empleado con ese Mail"); }

            paciente = await Context.Personas
                .FirstOrDefaultAsync(x => x.Dni == dto.Dni);

            if (paciente != null) { return (paciente.Id, Mensaje: "Ya existe un empleado con ese Dni"); }

            return (null, Mensaje: "");
        }
    }
}
