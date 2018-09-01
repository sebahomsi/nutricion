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
                Unidad = dto.Unidad
            };

            Context.MicroNutrienteDetalles.Add(micro);
            await Context.SaveChangesAsync();
            return micro.Id;
        }

        public async Task Update(MicroNutrienteDetalleDto dto)
        {
            var micro = Context.MicroNutrienteDetalles.Find(dto.Id);
            if (micro == null) throw new ArgumentNullException();

            micro.Codigo = dto.Codigo;
            micro.AlimentoId = dto.AlimentoId;
            micro.MicroNutrienteId = dto.MicroNutrienteId;
            micro.Cantidad = dto.Cantidad;
            micro.Unidad = dto.Unidad;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var detalle = Context.MicroNutrienteDetalles.Find(id);
            Context.MicroNutrienteDetalles.Remove(detalle);
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MicroNutrienteDetalleDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.MicroNutrienteDetalles
                .AsNoTracking()
                .Include("Alimento")
                .Include("MicroNutriente")
                .Where(x => x.Codigo == codigo /*|| x.Alimento.Descripcion.Contains(cadenaBuscar)*/)
                .Select(x => new MicroNutrienteDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    MicroNutrienteId = x.MicroNutrienteId,
                    MicroNutrienteStr = x.MicroNutriente.Descripcion,
                    Cantidad = x.Cantidad,
                    Unidad = x.Unidad
                }).ToListAsync();
        }

        public async Task<MicroNutrienteDetalleDto> GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
