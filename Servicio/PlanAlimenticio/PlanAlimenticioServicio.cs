using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;

namespace Servicio.PlanAlimenticio
{
    public class PlanAlimenticioServicio : ServicioBase, IPlanAlimenticioServicio
    {
        public async Task<long> Add(PlanAlimenticioDto dto)
        {
            var plan = new Dominio.Entidades.PlanAlimenticio()
            {
                Codigo = dto.Codigo,
                Fecha = DateTime.Today,
                Motivo = dto.Motivo,
                PacienteId = dto.PacienteId,
                Comentarios = dto.Comentarios,
                Eliminado = false
            };

            Context.PlanesAlimenticios.Add(plan);
            await Context.SaveChangesAsync();

            return plan.Id;
        }

        public async Task Update(PlanAlimenticioDto dto)
        {
            var plan = await Context.PlanesAlimenticios.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (plan == null) throw new ArgumentNullException();

            plan.Fecha = dto.Fecha;
            plan.Motivo = dto.Motivo;
            plan.PacienteId = dto.PacienteId;
            plan.Comentarios = dto.Comentarios;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var plan = await Context.PlanesAlimenticios.FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null) throw new ArgumentNullException();

            plan.Eliminado = !plan.Eliminado;
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PlanAlimenticioDto>> Get(string cadenaBuscar)
        {
            DateTime.TryParse(cadenaBuscar, out var fecha);
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.PlanesAlimenticios
                .AsNoTracking()
                .Include("Paciente")
                .Where(x => x.Fecha == fecha
                        || x.Codigo == codigo)
                .Select(x => new PlanAlimenticioDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Fecha = x.Fecha,
                    Motivo = x.Motivo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    Comentarios = x.Comentarios,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<PlanAlimenticioDto> GetById(long id)
        {
            var plan = await Context.PlanesAlimenticios.AsNoTracking()
                .Include("Paciente")
                .Include("Dias")
                .Include("Dias.PlanAlimenticio")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null) throw new ArgumentNullException();

            return new PlanAlimenticioDto()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.Paciente.Apellido + " " + plan.Paciente.Nombre,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                Dias = plan.Dias.Select(x=> new DiaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    PlanAlimenticioStr = x.PlanAlimenticio.Motivo
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.PlanesAlimenticios.AnyAsync()
                ? await Context.PlanesAlimenticios.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
