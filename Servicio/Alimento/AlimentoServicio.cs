using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Bridge;
using Servicio.Interface.Alimento;
using Servicio.Interface.MacroNutriente;

namespace Servicio.Alimento
{
    public class AlimentoServicio : ServicioBase, IAlimentoServicio
    {
        private readonly ILacteosService _lacteosService;

        public AlimentoServicio(ILacteosService lacteosService)
        {
            _lacteosService = lacteosService;
        }
        public async Task<long> Add(AlimentoDto dto)
        {
            var alimento = new Dominio.Entidades.Alimento()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false,
                SubGrupoId = dto.SubGrupoId,
                MacroNutriente = new Dominio.Entidades.MacroNutriente()
                {
                    Energia = dto.MacroNutriente.Energia,
                    Grasa = dto.MacroNutriente.Grasa,
                    HidratosCarbono = dto.MacroNutriente.HidratosCarbono,
                    Proteina = dto.MacroNutriente.Proteina,
                    Calorias = dto.MacroNutriente.Calorias,
                    Eliminado = false
                }
            };

            Context.Alimentos.Add(alimento);
            await Context.SaveChangesAsync();
            return alimento.Id;
        }

        public async Task Update(AlimentoDto dto)
        {
            var alimento = await Context.Alimentos.Include("MacroNutriente").FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (alimento == null) throw new ArgumentNullException();

            alimento.Descripcion = dto.Descripcion;
            alimento.SubGrupoId = dto.SubGrupoId;

            alimento.MacroNutriente.Energia = dto.MacroNutriente.Energia;
            alimento.MacroNutriente.Grasa = dto.MacroNutriente.Grasa;
            alimento.MacroNutriente.HidratosCarbono = dto.MacroNutriente.HidratosCarbono;
            alimento.MacroNutriente.Proteina = dto.MacroNutriente.Proteina;
            alimento.MacroNutriente.Calorias = dto.MacroNutriente.Calorias;

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
            //_lacteosService.ListarLacteos();
            Expression<Func<Dominio.Entidades.Alimento, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.Alimentos
                .AsNoTracking()
                .Include("SubGrupo")
                .Include("MacroNutriente")
                .Where(expression)
                .Select(x => new AlimentoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    SubGrupoId = x.SubGrupoId,
                    SubGrupoStr = x.SubGrupo.Descripcion,
                    Eliminado = x.Eliminado,
                }).ToListAsync();
        }

        public async Task<AlimentoDto> GetById(long id)
        {
            var alimento = await Context.Alimentos
                .AsNoTracking()
                .Include("SubGrupo")
                .Include("MacroNutriente")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alimento == null) throw new ArgumentNullException();

            return new AlimentoDto()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                Eliminado = alimento.Eliminado,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupo.Descripcion,
                MacroNutriente = new MacroNutrienteDto()
                {
                    Id = alimento.MacroNutriente.Id,
                    HidratosCarbono = alimento.MacroNutriente.HidratosCarbono,
                    Grasa = alimento.MacroNutriente.Grasa,
                    Proteina = alimento.MacroNutriente.Proteina,
                    Energia = alimento.MacroNutriente.Energia,
                    Calorias = alimento.MacroNutriente.Calorias
                }
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
