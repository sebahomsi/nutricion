using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Grupo;

namespace Servicio.Grupo
{
    public class GrupoServicio : ServicioBase, IGrupoServicio
    {
        public async Task<long> Add(GrupoDto dto)
        {
            var grupo = new Dominio.Entidades.Grupo()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Grupos.Add(grupo);
            await Context.SaveChangesAsync();
            return grupo.Id;
        }

        public async Task Update(GrupoDto dto)
        {
            var grupo = Context.Grupos.Find(dto.Id);
            if (grupo == null) throw new ArgumentNullException();

            grupo.Codigo = dto.Codigo;
            grupo.Descripcion = dto.Descripcion;
            grupo.Eliminado = dto.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var grupo = Context.Grupos.Find(id);
            if (grupo == null) throw new ArgumentNullException();

            grupo.Eliminado = !grupo.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<GrupoDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.Grupos
                .AsNoTracking()
                .Where(x => x.Codigo == codigo).Select(x => new GrupoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<GrupoDto> GetById(long id)
        {
            var grupo = await Context.Grupos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (grupo == null) throw new ArgumentNullException();

            return new GrupoDto()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            };
        }
    }
}
