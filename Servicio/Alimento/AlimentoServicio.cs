using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;

namespace Servicio.Alimento
{
    public class AlimentoServicio : ServicioBase, IAlimentoServicio
    {
        public async Task<long> Add(AlimentoDto dto)
        {
            var alimento = new Dominio.Entidades.Alimento()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false,
                MacroNutrienteId = null,
                TieneMacroNutriente = false,
                SubGrupoId = dto.SubGrupoId
            };

            Context.Alimentos.Add(alimento);
            await Context.SaveChangesAsync();
            return alimento.Id;
        }

        public async Task Update(AlimentoDto dto)
        {
            var alimento = Context.Alimentos.Find(dto.Id);
            if (alimento == null) throw new ArgumentNullException();

            alimento.Codigo = dto.Codigo; //no se modifica
            alimento.Descripcion = dto.Descripcion;
            alimento.SubGrupoId = dto.SubGrupoId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var alimento = Context.Alimentos.Find(id);
            if (alimento == null) throw new ArgumentNullException();

            alimento.Eliminado = !alimento.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<AlimentoDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.Alimentos
                .AsNoTracking()
                .Where(x => x.Descripcion.Contains(cadenaBuscar)
                            || x.Codigo == codigo)
                .Select(x => new AlimentoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    SubGrupoId = x.SubGrupoId,
                    MacroNutrienteId = x.MacroNutrienteId,
                    TieneMacroNutriente = x.TieneMacroNutriente,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<AlimentoDto> GetById(long id)
        {
            var alimento = await Context.Alimentos
                .AsNoTracking()
                .Include("SubGrupo")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alimento == null) throw new ArgumentNullException();

            return new AlimentoDto()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                Eliminado = alimento.Eliminado,
                MacroNutrienteId = alimento.MacroNutrienteId,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupo.Descripcion,
                TieneMacroNutriente = alimento.TieneMacroNutriente
            };
        }
    }
}
