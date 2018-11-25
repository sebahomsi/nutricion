using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutrienteDetalle;

namespace Servicio.MicroNutrienteDetalle
{
    public class MicroNutrienteDetalleServicio : ServicioBase, IMicroNutrienteDetalleServicio
    {
        public async Task<long> Add(MicroNutrienteDetalleDto dto)
        {
            var micro = new Dominio.Entidades.MicroNutrienteDetalle()
            {
                Codigo = dto.Codigo,
                AlimentoId = dto.AlimentoId,
                MicroNutrienteId = dto.MicroNutrienteId,
                Cantidad = dto.Cantidad,
                UnidadMedidaId = dto.UnidadMedidaId
            };

            Context.MicroNutrienteDetalles.Add(micro);
            await Context.SaveChangesAsync();
            return micro.Id;
        }

        public async Task Update(MicroNutrienteDetalleDto dto)
        {
            var micro = await Context.MicroNutrienteDetalles.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (micro == null) throw new ArgumentNullException();

            micro.AlimentoId = dto.AlimentoId;
            micro.MicroNutrienteId = dto.MicroNutrienteId;
            micro.Cantidad = dto.Cantidad;
            micro.UnidadMedidaId = dto.UnidadMedidaId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var detalle = await Context.MicroNutrienteDetalles.FirstOrDefaultAsync(x => x.Id == id);
            Context.MicroNutrienteDetalles.Remove(detalle);
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MicroNutrienteDetalleDto>> Get(string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.MicroNutrienteDetalles
                .AsNoTracking()
                .Include("Alimento")
                .Include("MicroNutriente")
                .Include("UnidadMedida")
                .Where(x => x.Codigo == codigo || x.Alimento.Descripcion.Contains(cadenaBuscar))
                .Select(x => new MicroNutrienteDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    MicroNutrienteId = x.MicroNutrienteId,
                    MicroNutrienteStr = x.MicroNutriente.Descripcion,
                    Cantidad = x.Cantidad,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedida.Abreviatura
                }).ToListAsync();
        }

        public async Task<MicroNutrienteDetalleDto> GetById(long id)
        {
            var detalle = await Context.MicroNutrienteDetalles
                .AsNoTracking()
                .Include("Alimento")
                .Include("MicroNutriente")
                .Include("UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == id);

            if(detalle == null) throw new ArgumentNullException();

            return new MicroNutrienteDetalleDto()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.Alimento.Descripcion,
                MicroNutrienteId = detalle.MicroNutrienteId,
                MicroNutrienteStr = detalle.MicroNutriente.Descripcion,
                Cantidad = detalle.Cantidad,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedida.Abreviatura
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.MicroNutrienteDetalles.AnyAsync()
                ? await Context.MicroNutrienteDetalles.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
