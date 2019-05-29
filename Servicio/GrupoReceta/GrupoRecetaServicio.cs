using Servicio.Interface.GrupoReceta;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicio.GrupoReceta
{
    public class GrupoRecetaServicio : ServicioBase, IGrupoRecetaServicio
    {
        public async Task<long> Add(GrupoRecetaDto dto)
        {
            var grupo = new Dominio.Entidades.GrupoReceta()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.GruposRecetas.Add(grupo);
            await Context.SaveChangesAsync();
            return grupo.Id;
        }

        public async Task Update(GrupoRecetaDto dto)
        {
            var grupo = await Context.GruposRecetas.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (grupo == null) throw new ArgumentNullException();

            grupo.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var grupo = await Context.GruposRecetas.FirstOrDefaultAsync(x => x.Id == id);
            if (grupo == null) throw new ArgumentNullException();

            grupo.Eliminado = !grupo.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<GrupoRecetaDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.GrupoReceta, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.GruposRecetas
                .AsNoTracking()
                .Where(expression).Select(x => new GrupoRecetaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<GrupoRecetaDto> GetById(long id)
        {
            var grupo = await Context.GruposRecetas
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (grupo == null) throw new ArgumentNullException();

            return new GrupoRecetaDto()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.GruposRecetas.AnyAsync()
                ? await Context.GruposRecetas.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
