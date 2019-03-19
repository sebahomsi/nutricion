using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (alimento == null) throw new ArgumentNullException();

            alimento.Descripcion = dto.Descripcion;
            alimento.SubGrupoId = dto.SubGrupoId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var alimento = await Context.Alimentos.FirstOrDefaultAsync(x=> x.Id == id);
            if (alimento == null) throw new ArgumentNullException();

            alimento.Eliminado = !alimento.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<AlimentoDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Alimento, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.Alimentos
                .AsNoTracking()
                .Include("SubGrupo")
                .Where(expression)
                .Select(x => new AlimentoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    SubGrupoId = x.SubGrupoId,
                    SubGrupoStr = x.SubGrupo.Descripcion,
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

        public async Task<ICollection<AlimentoDto>> GetFoodJson(string term)
        {
            return await Context.Alimentos
                .Where(x => x.Descripcion.Contains(term))
                .Select(x => new AlimentoDto()
                {
                    Descripcion = x.Descripcion,
                    Id = x.Id
                }).Take(5).ToListAsync();
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Alimentos.AnyAsync()
                ? await Context.Alimentos.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
