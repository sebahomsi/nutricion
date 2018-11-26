using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.Opcion;
using Servicio.Interface.SubGrupo;

namespace Servicio.SubGrupo
{
    public class SubGrupoServicio: ServicioBase, ISubGrupoServicio
    {
        public async Task<long> Add(SubGrupoDto dto)
        {
            var sub = new Dominio.Entidades.SubGrupo()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                GrupoId = dto.GrupoId,
                Eliminado = false
            };

            Context.SubGrupos.Add(sub);
            await Context.SaveChangesAsync();

            return sub.Id;
        }

        public async Task Update(SubGrupoDto dto)
        {
            var sub = await Context.SubGrupos.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (sub == null) throw new ArgumentNullException();

            sub.Descripcion = dto.Descripcion;
            sub.GrupoId = dto.GrupoId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var sub = await Context.SubGrupos.FirstOrDefaultAsync(x => x.Id == id);
            if (sub == null) throw new ArgumentNullException();

            sub.Eliminado = !sub.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<SubGrupoDto>> Get(string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.SubGrupos.AsNoTracking()
                .Include("Alimentos")
                .Include("Grupo")
                .Where(x => x.Descripcion.Contains(cadenaBuscar)
                        || x.Codigo == codigo)
                .Select(x => new SubGrupoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    GrupoId = x.GrupoId,
                    GrupoStr = x.Grupo.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<SubGrupoDto> GetById(long id)
        {
            var sub = await Context.SubGrupos.AsNoTracking()
                .Include("Alimentos")
                .Include("Grupo")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sub == null) throw new ArgumentNullException();

            return new SubGrupoDto()
            {
                Id = sub.Id,
                Codigo = sub.Codigo,
                Descripcion = sub.Descripcion,
                GrupoId = sub.GrupoId,
                GrupoStr = sub.Grupo.Descripcion,
                Alimentos = sub.Alimentos.Select(t=> new AlimentoDto()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion,
                    SubGrupoId = t.SubGrupoId,
                    SubGrupoStr = t.SubGrupo.Descripcion,
                    MacroNutrienteId = t.MacroNutrienteId,
                    TieneMacroNutriente = t.TieneMacroNutriente,
                    MicroNutrientes = t.MicroNutrientes.Select(r=> new MicroNutrienteDto()
                    {
                        Id = r.Id,
                        Codigo = r.Codigo,
                        Descripcion = r.Descripcion
                    }).ToList(),
                    Eliminado = t.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.SubGrupos.AnyAsync() ? await Context.SubGrupos.MaxAsync(x => x.Codigo) + 1 : 1;
        }
    }
}
