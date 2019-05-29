using Servicio.Interface.Alimento;
using Servicio.Interface.SubGrupo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Servicio.Interface.Opcion;
using Servicio.Interface.SubGrupoReceta;

namespace Servicio.SubGrupoReceta
{
    public class SubGrupoRecetaServicio : ServicioBase, ISubGrupoRecetaServicio
    {
        public async Task<long> Add(SubGrupoRecetaDto dto)
        {
            var sub = new Dominio.Entidades.SubGrupoReceta()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                GrupoRecetaId = dto.GrupoRecetaId,
                Eliminado = false
            };

            Context.SubGruposRecetas.Add(sub);
            await Context.SaveChangesAsync();

            return sub.Id;
        }

        public async Task Update(SubGrupoRecetaDto dto)
        {
            var sub = await Context.SubGruposRecetas.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (sub == null) throw new ArgumentNullException();

            sub.Descripcion = dto.Descripcion;
            sub.GrupoRecetaId = dto.GrupoRecetaId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var sub = await Context.SubGruposRecetas.FirstOrDefaultAsync(x => x.Id == id);
            if (sub == null) throw new ArgumentNullException();

            sub.Eliminado = !sub.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<SubGrupoRecetaDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.SubGrupoReceta, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.SubGruposRecetas.AsNoTracking()
                .Include("Grupo")
                .Where(expression)
                .OrderBy(x => x.GrupoRecetaId)
                .Select(x => new SubGrupoRecetaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    GrupoRecetaId = x.GrupoRecetaId,
                    GrupoRecetaStr = x.GrupoReceta.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<SubGrupoRecetaDto> GetById(long id)
        {
            var sub = await Context.SubGruposRecetas.AsNoTracking()
                .Include("Opciones")
                .Include("GrupoReceta")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sub == null) throw new ArgumentNullException();

            return new SubGrupoRecetaDto()
            {
                Id = sub.Id,
                Codigo = sub.Codigo,
                Descripcion = sub.Descripcion,
                GrupoRecetaId = sub.GrupoRecetaId,
                GrupoRecetaStr = sub.GrupoReceta.Descripcion,
                Eliminado = sub.Eliminado,
                Opciones = sub.Opciones.Select(t => new OpcionDto()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion,
                    Eliminado = t.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.SubGruposRecetas.AnyAsync() ? await Context.SubGruposRecetas.MaxAsync(x => x.Codigo) + 1 : 1;
        }
    }
}
