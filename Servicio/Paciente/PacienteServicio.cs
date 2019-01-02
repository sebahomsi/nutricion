using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Turno;

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
            };

            Context.Personas.Add(paciente);
            await Context.SaveChangesAsync();

            return paciente.Id;
        }

        public async Task Update(PacienteDto dto)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>().FirstOrDefaultAsync(x => x.Id == dto.Id);
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
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>().FirstOrDefaultAsync(x => x.Id == id);
            if (paciente == null) throw new ArgumentNullException();

            paciente.Eliminado =! paciente.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PacienteDto>> Get(string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .AsNoTracking()
                .Where(x => x.Nombre.Contains(cadenaBuscar)
                            || x.Apellido.Contains(cadenaBuscar)
                            || x.Codigo == codigo)
                .Select(x => new PacienteDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Apellido = x.Apellido,
                    Nombre = x.Nombre,
                    Celular = x.Celular,
                    Dni = x.Dni,
                    Direccion = x.Direccion,
                    Mail = x.Mail,
                    Telefono = x.Telefono,
                    Sexo = x.Sexo,
                    FechaNac = x.FechaNac,
                    Foto = x.Foto,
                    Eliminado = x.Eliminado,
                    Estado = x.Estado,
                }).ToListAsync();
        }

        public async Task<PacienteDto> GetById(long id)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .AsNoTracking()
                .Include("DatosAntropometricos")
                .Include("DatosAntropometricos.Paciente")
                .Include("PlanesAlimenticios")
                .Include("Turnos")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (paciente == null) throw new ArgumentNullException();

            return new PacienteDto()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Dni = paciente.Dni,
                Direccion = paciente.Direccion,
                Mail = paciente.Mail,
                Telefono = paciente.Telefono,
                Sexo = paciente.Sexo,
                FechaNac = paciente.FechaNac,
                Foto = paciente.Foto,
                Eliminado = paciente.Eliminado,
                Estado = paciente.Estado,
                DatosAntropometricos = paciente.DatosAntropometricos.Select(p=> new DatoAntropometricoDto()
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    PacienteId = p.PacienteId,
                    PacienteStr = p.Paciente.Apellido + " " + p.Paciente.Nombre,
                    Altura = p.Altura,
                    FechaMedicion = p.FechaMedicion,
                    MasaGrasa = p.MasaGrasa,
                    MasaCorporal = p.MasaCorporal,
                    Peso = p.Peso,
                    PerimetroCintura = p.PerimetroCintura,
                    PerimetroCadera = p.PerimetroCadera,
                    Eliminado = p.Eliminado
                }).ToList(),
                PlanesAlimenticios = paciente.PlanesAlimenticios.Select(q=> new PlanAlimenticioDto()).ToList(),
                Turnos = paciente.Turnos.Select(t=> new TurnoDto()).ToList(),
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Personas.OfType<Dominio.Entidades.Paciente>().AnyAsync()
                ? await Context.Personas.OfType<Dominio.Entidades.Paciente>().MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
