using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Estrategia;

namespace Servicio.Estrategia
{
    public class EstrategiaServicio : ServicioBase, IEstrategiaServicio
    {
        public async Task Add(EstrategiaDto dto)
        {
            var estrategia = new Dominio.Entidades.Estrategia()
            {
                Descripcion = dto.Descripcion,
                PacienteId = dto.PacienteId
            };

            Context.Estrategias.Add(estrategia);
            await Context.SaveChangesAsync();
        }

        public async Task Update(EstrategiaDto dto)
        {
            var estrategia = Context.Estrategias.FirstOrDefault(x => x.Id == dto.Id);

            estrategia.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task<EstrategiaDto> GetByPacienteId(long id)
        {
            var estrategia = await Context.Estrategias.FirstOrDefaultAsync(x => x.PacienteId == id);

            if (estrategia == null) return new EstrategiaDto()
            {
                Descripcion = "Estrategia",
                PacienteId = id,
                Id = 0
            };

            return new EstrategiaDto()
            {
                Descripcion = estrategia.Descripcion,
                Id = estrategia.Id,
                PacienteId = estrategia.PacienteId
            };
        }
    }
}
