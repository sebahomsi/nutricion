using Servicio.Interface.Turno;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

namespace Servicio.Turno
{
    public class TurnoServicio : ServicioBase, ITurnoServicio
    {
        public async Task<long> Add(TurnoDto dto)
        {
            var turno = new Dominio.Entidades.Turno()
            {
                Numero = dto.Numero,
                PacienteId = dto.PacienteId,
                EstadoId = dto.EstadoId,
                HorarioEntrada = dto.HorarioEntrada,
                HorarioSalida = dto.HorarioSalida,
                Motivo = dto.Motivo,
                Eliminado = false
            };
            Context.Turnos.Add(turno);
            await Context.SaveChangesAsync();

            return turno.Id;
        }

        public async Task Update(TurnoDto dto)
        {
            var turno = await Context.Turnos.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (turno == null) throw new ArgumentNullException();

            turno.PacienteId = dto.PacienteId;
            turno.HorarioEntrada = dto.HorarioEntrada;
            turno.HorarioSalida = dto.HorarioSalida;
            turno.EstadoId = dto.EstadoId;
            turno.Motivo = dto.Motivo;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var turno = await Context.Turnos.FirstOrDefaultAsync(x => x.Id == id);
            if (turno == null) throw new ArgumentNullException();

            turno.Eliminado = !turno.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<TurnoDto>> Get(bool eliminado,string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Turno, bool>> expression = x => x.Eliminado == eliminado && (x.Paciente.Nombre.Contains(cadenaBuscar) || x.Paciente.Apellido.Contains(cadenaBuscar));


            return await Context.Turnos.AsNoTracking()
                .Include("Paciente")
                .Include("Estado")
                .Where(expression)
                .OrderBy(x=> x.HorarioEntrada)
                .Select(x => new TurnoDto()
                {
                    Id = x.Id,
                    Numero = x.Numero,
                    PacienteId = x.PacienteId,

                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    HorarioEntrada = x.HorarioEntrada,
                    HorarioSalida = x.HorarioSalida,
                    EstadoColor = x.Estado.Color,
                    EstadoDescripcion = x.Estado.Descripcion,
                    EstadoId = x.EstadoId,
                    Motivo = x.Motivo,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<TurnoDto> GetById(long id)
        {
            var turno = await Context.Turnos.AsNoTracking()
                .Include("Paciente")
                .Include("Estado")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (turno == null) throw new ArgumentNullException();

            return new TurnoDto()
            {
                Id = turno.Id,
                Numero = turno.Numero,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.Paciente.Apellido + " " + turno.Paciente.Nombre,
                HorarioEntrada = turno.HorarioEntrada,
                HorarioSalida = turno.HorarioSalida,
                Motivo = turno.Motivo,
                Eliminado = turno.Eliminado,
                EstadoId = turno.EstadoId,
                EstadoDescripcion = turno.Estado.Descripcion,
                EstadoColor = turno.Estado.Color
            };

        }

        public async Task<int> GetNextCode()
        {
            return await Context.Turnos.AnyAsync() ? await Context.Turnos.MaxAsync(x => x.Numero) + 1 : 1;
        }

        public async Task<IEnumerable<TurnoDto>> GetByIdPaciente(long id)
        {
            var datos = await Context.Turnos.Where(x => x.PacienteId == id).ToListAsync();

            var turnos = Mapper.Map<IEnumerable<TurnoDto>>(datos);

            return turnos;
        }

    }
}
