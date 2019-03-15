using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.Receta;
using Servicio.Interface.RecetaDetalle;

namespace Servicio.Receta
{
    public class RecetaServicio : ServicioBase, IRecetaServicio
    {
        public async Task<long> Add(RecetaDto dto)
        {
            var receta = new Dominio.Entidades.Receta()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Recetas.Add(receta);
            await Context.SaveChangesAsync();

            return receta.Id;
        }

        public async Task Update(RecetaDto dto)
        {
            var receta = await Context.Recetas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            receta.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var receta = await Context.Recetas.FirstOrDefaultAsync(x => x.Id == id);

            receta.Eliminado = !receta.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<RecetaDto>> Get(bool eliminado, string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.Receta, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.Recetas.AsNoTracking()
                .Include("RecetasDetalles.Alimento")
                .Include("RecetasDetalles.UnidadMedida")
                .Where(expression).Select(x => new RecetaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado,
                    RecetasDetalles = x.RecetasDetalles.Select(q=> new RecetaDetalleDto()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        AlimentoId = q.AlimentoId,
                        AlimentoStr = q.Alimento.Descripcion,
                        UnidadMedidaId = q.UnidadMedidaId,
                        UnidadMedidaStr = q.UnidadMedida.Abreviatura,
                        Cantidad = q.Cantidad,
                        Eliminado = q.Eliminado
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<RecetaDto> GetById(long id)
        {
            var receta = await Context.Recetas
                .AsNoTracking()
                .Include("RecetasDetalles.Alimento")
                .Include("RecetasDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == id);

            return new RecetaDto()
            {
                Id = receta.Id,
                Codigo = receta.Codigo,
                Descripcion = receta.Descripcion,
                Eliminado = receta.Eliminado,
                RecetasDetalles = receta.RecetasDetalles.Where(q => q.Eliminado == false).Select(q => new RecetaDetalleDto()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    AlimentoId = q.AlimentoId,
                    AlimentoStr = q.Alimento.Descripcion,
                    UnidadMedidaId = q.UnidadMedidaId,
                    UnidadMedidaStr = q.UnidadMedida.Abreviatura,
                    Cantidad = q.Cantidad,
                    Eliminado = q.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Recetas.AnyAsync()
                ? await Context.Recetas.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
