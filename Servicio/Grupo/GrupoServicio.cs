using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public async Task<ICollection<GrupoDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<GrupoDto> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
