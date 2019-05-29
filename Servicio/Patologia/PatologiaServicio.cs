using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Servicio.Interface.Observacion;
using Servicio.Interface.Patologia;

namespace Servicio.Patologia
{
    public class PatologiaServicio : ServicioBase, IPatologiaServicio
    {
        public async Task<long> Add(PatologiaDto dto)
        {
            var patologia = new Dominio.Entidades.Patologia()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Patologias.Add(patologia);
            await Context.SaveChangesAsync();
            return patologia.Id;
        }

        public async Task Update(PatologiaDto dto)
        {
            var patologia = await Context.Patologias.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (patologia == null) throw new ArgumentNullException();

            patologia.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var patologia = await Context.Patologias.FirstOrDefaultAsync(x => x.Id == id);
            if (patologia == null) throw new ArgumentNullException();

            patologia.Eliminado =! patologia.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PatologiaDto>> Get(bool eliminado,string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Patologia, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.Patologias.AsNoTracking()
                .Where(expression)
                .Select(x => new PatologiaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<PatologiaDto> GetById(long id)
        {
            var patologia = await Context.Patologias
                .AsNoTracking()
                .Include("Observaciones.Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);
            if(patologia == null) throw new ArgumentNullException();

            return new PatologiaDto()
            {
                Id = patologia.Id,
                Codigo = patologia.Codigo,
                Descripcion = patologia.Descripcion,
                Eliminado = patologia.Eliminado,
                Observaciones = patologia.Observaciones.Select(x=> new ObservacionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido + " " + x.Paciente.Nombre,
                    Tabaco = x.Tabaco,
                    Alcohol = x.Alcohol,
                    AntecedentesFamiliares = x.AntecedentesFamiliares,
                    ActividadFisica = x.ActividadFisica,
                    RitmoEvacuatorio = x.RitmoEvacuatorio,
                    Medicacion = x.Medicacion,
                    HorasSuenio = x.HorasSuenio,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Patologias.AnyAsync()
                ? await Context.Patologias.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
