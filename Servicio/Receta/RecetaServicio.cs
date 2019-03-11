using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.Receta;

namespace Servicio.Receta
{
    public class RecetaServicio : ServicioBase, IRecetaServicio
    {
        public async Task<long> Add(RecetaDto dto)
        {
            var receta = new Dominio.Entidades.Receta()
            {
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Recetas.Add(receta);
            await Context.SaveChangesAsync();

            return receta.Id;
        }

        public async Task Update(RecetaDto dto)
        {
            var receta = await Context.Recetas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            receta.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var receta = await Context.Recetas.FirstOrDefaultAsync(x => x.Id == id);

            receta.Eliminado = !receta.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<RecetaDto>> Get(bool eliminado, string cadenaBuscar)
        {
            return await Context.Recetas.AsNoTracking()
                .Include("Alimentos")
                .Where(x => x.Descripcion.Contains(cadenaBuscar)).Select(x => new RecetaDto()
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado,
                    Alimentos = x.Alimentos.Select(q=> new AlimentoDto()
                    {
                        Id = q.Id,
                        Descripcion = q.Descripcion,
                        Eliminado = q.Eliminado
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<RecetaDto> GetById(long id)
        {
            var receta = await Context.Recetas
                .AsNoTracking()
                .Include("Alimentos").FirstOrDefaultAsync(x => x.Id == id);

            return new RecetaDto()
            {
                Id = receta.Id,
                Descripcion = receta.Descripcion,
                Eliminado = receta.Eliminado,
                Alimentos = receta.Alimentos.Select(x=> new AlimentoDto()
                {
                    Id = x.Id,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Recetas.AnyAsync()
                ? await Context.Recetas.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
