using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
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
                Energia = dto.Energia,
                Grasa = dto.Grasa,
                HidratosCarbono = dto.HidratosCarbono,
                Proteina = dto.Proteina,
                Calorias = dto.Calorias,
                Eliminado = false
            };

            Context.MacroNutrientes.Add(macro);

            await Context.SaveChangesAsync();


            await Context.SaveChangesAsync();

            return macro.Id;
        }

        public async Task Update(MacroNutrienteDto dto)
        {
            var macro = await Context.MacroNutrientes.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (macro == null) throw new ArgumentNullException();

            macro.Energia = dto.Energia;
            macro.Grasa = dto.Grasa;
            macro.HidratosCarbono = dto.HidratosCarbono;
            macro.Proteina = dto.Proteina;
            macro.Calorias = dto.Calorias;
            
            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var macro = await Context.MacroNutrientes.FirstOrDefaultAsync(x => x.Id == id);
            if (macro == null) throw new ArgumentNullException();

            macro.Eliminado = !macro.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MacroNutrienteDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.MacroNutrientes
                .AsNoTracking()
                .Include("Alimento")
                .Where(x => x.Codigo == codigo)
                .Select(x => new MacroNutrienteDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoStr = x.Alimento.Descripcion,
                    Energia = x.Energia,
                    Grasa = x.Grasa,
                    HidratosCarbono = x.HidratosCarbono,
                    Proteina = x.Proteina,
                    Calorias = x.Calorias,
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
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                HidratosCarbono = macro.HidratosCarbono,
                Proteina = macro.Proteina,
                Calorias = macro.Calorias,
                Eliminado = macro.Eliminado,
                AlimentoStr = macro.Alimento.Descripcion
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.MacroNutrientes.AnyAsync()
                ? await Context.MacroNutrientes.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
