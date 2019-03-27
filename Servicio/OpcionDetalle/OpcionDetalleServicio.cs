﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.OpcionDetalle
{
    public class OpcionDetalleServicio : ServicioBase, IOpcionDetalleServicio
    {
        public async Task<long> Add(OpcionDetalleDto dto)
        {
            var verify = await VerifyDuplicity(dto);

            if (verify.HasValue)
            {
                await IncreaseAmount(verify.Value, dto.Cantidad);
            }
            else
            {
                var detalle = new Dominio.Entidades.OpcionDetalle()
                {
                    Codigo = dto.Codigo,
                    AlimentoId = dto.AlimentoId,
                    UnidadMedidaId = dto.UnidadMedidaId,
                    Cantidad = dto.Cantidad,
                    OpcionId = dto.OpcionId,
                    Eliminado = false
                };

                Context.OpcionesDetalles.Add(detalle);
                await Context.SaveChangesAsync();
                return detalle.Id;
            }
            return verify.Value;
        }
            
           

        public async Task Update(OpcionDetalleDto dto)
        {
            var detalle = await Context.OpcionesDetalles.FirstOrDefaultAsync(x=> x.Id == dto.Id);
            if (detalle == null) throw new ArgumentNullException();

            detalle.AlimentoId = dto.AlimentoId;
            detalle.UnidadMedidaId = dto.UnidadMedidaId;
            detalle.Cantidad = dto.Cantidad;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var detalle = await Context.OpcionesDetalles.FirstOrDefaultAsync(x => x.Id == id);
            if (detalle == null) throw new ArgumentNullException();

            detalle.Eliminado = !detalle.Eliminado;

            await Context.SaveChangesAsync();

        }

        public async Task<ICollection<OpcionDetalleDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.OpcionDetalle, bool>> expression = x => x.Eliminado == eliminado && x.Alimento.Descripcion.Contains(cadenaBuscar);

            return await Context.OpcionesDetalles.AsNoTracking()
                .Include("Alimento")
                .Include("UnidadMedida")
                .Include("Opcion")
                .Where(expression)
                .Select(x => new OpcionDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedida.Abreviatura,
                    Cantidad = x.Cantidad,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.Opcion.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<OpcionDetalleDto> GetById(long id)
        {
            var detalle = await Context.OpcionesDetalles.AsNoTracking()
                .Include("Alimento")
                .Include("UnidadMedida")
                .Include("Opcion")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (detalle == null) throw new ArgumentNullException();

            return new OpcionDetalleDto()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.Alimento.Descripcion,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedida.Abreviatura,
                Cantidad = detalle.Cantidad,
                OpcionId = detalle.OpcionId,
                OpcionStr = detalle.Opcion.Descripcion,
                Eliminado = detalle.Eliminado
            };

        }

        public async Task<int> GetNextCode()
        {
            return await Context.OpcionesDetalles.AnyAsync()
                ? await Context.OpcionesDetalles.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<long?> VerifyDuplicity(OpcionDetalleDto dto)
        {
            var id = await Context.OpcionesDetalles
                .FirstOrDefaultAsync(x => x.AlimentoId==dto.AlimentoId&&x.OpcionId==dto.OpcionId&&x.UnidadMedidaId==dto.UnidadMedidaId);

            return id?.Id;
        }

        public async Task IncreaseAmount(long id ,double cantidad)
        {
            var detalle = await Context.OpcionesDetalles.FirstOrDefaultAsync(x => x.Id == id);

            detalle.Cantidad = detalle.Cantidad + cantidad;

            await Context.SaveChangesAsync();



        }

       
    }
}
