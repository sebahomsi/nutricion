using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.MicroNutriente;

namespace Servicio.MicroNutriente
{
    public class MicroNutrienteServicio: ServicioBase, IMicroNutrienteServicio
    {
        public async Task<long> Add(MicroNutrienteDto dto)
        {
            var micro = new Dominio.Entidades.MicroNutriente()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.MicroNutrientes.Add(micro);
            await Context.SaveChangesAsync();
            return micro.Id;
        }

        public async Task Update(MicroNutrienteDto dto)
        {
            var micro = Context.MicroNutrientes.Find(dto.Id);
            if (micro == null) throw new ArgumentNullException();

            micro.Codigo = dto.Codigo;
            micro.Descripcion = dto.Descripcion;
            micro.Eliminado = dto.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var micro = Context.MicroNutrientes.Find(id);
            if (micro == null) throw new ArgumentNullException();

            micro.Eliminado = !micro.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MicroNutrienteDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.MicroNutrientes
                .AsNoTracking()
                .Where(x => x.Descripcion.Contains(cadenaBuscar) || x.Codigo == codigo)
                .Select(x => new MicroNutrienteDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<MicroNutrienteDto> GetById(long id)
        {
            var micro = await Context.MicroNutrientes
                .AsNoTracking()
                .Include("Alimentos")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (micro == null) throw new ArgumentNullException();

            return new MicroNutrienteDto()
            {
                Id = micro.Id,
                Codigo = micro.Codigo,
                Descripcion = micro.Descripcion,
                Eliminado = micro.Eliminado,
                Alimentos = micro.Alimentos.Select(x=> new AlimentoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    SubGrupoId = x.SubGrupoId,
                    MacroNutrienteId = x.MacroNutrienteId,
                    TieneMacroNutriente = x.TieneMacroNutriente,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }
    }
}
