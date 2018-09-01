using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MacroNutriente;

namespace Servicio.MacroNutriente
{
    public class MacroNutrienteServicio : ServicioBase, IMacroNutrienteServicio
    {
        public async Task<long> Add(MacroNutrienteDto dto)
        {
            var macro = new Dominio.Entidades.MacroNutriente()
            {
                Codigo = dto.Codigo,
                AlimentoId = dto.AlimentoId,
                Energia = dto.Energia,
                Grasa = dto.Grasa,
                HidratosCarbono = dto.HidratosCarbono,
                Proteina = dto.Proteina,
                Eliminado = false
            };

            Context.MacroNutrientes.Add(macro);
            await Context.SaveChangesAsync();

            return macro.Id;
        }

        public async Task Update(MacroNutrienteDto dto)
        {
            var macro = Context.MacroNutrientes.Find(dto.Id);
            if (macro == null) throw new ArgumentNullException();

            macro.Codigo = dto.Codigo;
            macro.AlimentoId = dto.AlimentoId;
            macro.Energia = dto.Energia;
            macro.Grasa = dto.Grasa;
            macro.HidratosCarbono = dto.HidratosCarbono;
            macro.Proteina = dto.Proteina;
            macro.Eliminado = dto.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var macro = Context.MacroNutrientes.Find(id);
            if (macro == null) throw new ArgumentNullException();

            macro.Eliminado = !macro.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MacroNutrienteDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.MacroNutrientes
                .AsNoTracking()
                .Where(x => x.Codigo == codigo)
                .Select(x => new MacroNutrienteDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    Energia = x.Energia,
                    Grasa = x.Grasa,
                    HidratosCarbono = x.HidratosCarbono,
                    Proteina = x.Proteina,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<MacroNutrienteDto> GetById(long id)
        {
            var macro = await Context.MacroNutrientes
                .AsNoTracking()
                .Include("Alimento")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (macro == null) throw new ArgumentNullException();

            return new MacroNutrienteDto()
            {
                Id = macro.Id,
                Codigo = macro.Codigo,
                AlimentoId = macro.AlimentoId,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                HidratosCarbono = macro.HidratosCarbono,
                Proteina = macro.Proteina,
                Eliminado = macro.Eliminado,
                AlimentoStr = macro.Alimento.Descripcion
            };
        }
    }
}
