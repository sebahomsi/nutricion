using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.RecetaDetalle;

namespace Servicio.RecetaDetalle
{
    public class RecetaDetalleServicio : ServicioBase, IRecetaDetalleServicio
    {
        public async Task<long> Add(RecetaDetalleDto dto)
        {
            var detalle = new Dominio.Entidades.RecetaDetalle()
            {
                Codigo = dto.Codigo,
                RecetaId = dto.RecetaId,
                AlimentoId = dto.AlimentoId,
                UnidadMedidaId = dto.UnidadMedidaId,
                Cantidad = dto.Cantidad,
                Eliminado = false
            };

            Context.RecetasDetalles.Add(detalle);
            await Context.SaveChangesAsync();
            return detalle.Id;
        }

        public async Task Update(RecetaDetalleDto dto)
        {
            var detalle = await Context.RecetasDetalles.FirstOrDefaultAsync(x => x.Id == dto.Id);

            detalle.AlimentoId = dto.AlimentoId;
            detalle.UnidadMedidaId = dto.UnidadMedidaId;
            detalle.Cantidad = dto.Cantidad;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var detalle = await Context.RecetasDetalles.FirstOrDefaultAsync(x => x.Id == id);

            detalle.Eliminado = !detalle.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<RecetaDetalleDto>> Get(bool eliminado, string cadenaBuscar)
        {
            return await Context.RecetasDetalles.AsNoTracking()
                .Include("Alimento")
                .Include("UnidadMedida")
                .Include("Receta")
                .Where(x => x.Alimento.Descripcion.Contains(cadenaBuscar)).Select(x => new RecetaDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    RecetaId = x.RecetaId,
                    RecetaStr = x.Receta.Descripcion,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedida.Abreviatura,
                    Cantidad = x.Cantidad,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<RecetaDetalleDto> GetById(long id)
        {
            var detalle = await Context.RecetasDetalles
                .AsNoTracking()
                .Include("Alimento")
                .Include("Receta")
                .Include("UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == id);

            return new RecetaDetalleDto()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                RecetaId = detalle.RecetaId,
                RecetaStr = detalle.Receta.Descripcion,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.Alimento.Descripcion,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedida.Descripcion,
                Cantidad = detalle.Cantidad,
                Eliminado = detalle.Eliminado
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.RecetasDetalles.AnyAsync()
                ? await Context.RecetasDetalles.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
