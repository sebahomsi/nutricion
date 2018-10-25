using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.UnidadMedida;

namespace Servicio.UnidadMedida
{
    public class UnidadMedidaServicio: ServicioBase, IUnidadMedidaServicio
    {
        public async Task<long> Add(UnidadMedidaDto dto)
        {
            var unidad = new Dominio.Entidades.UnidadMedida()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Abreviatura = dto.Abreviatura,
                Eliminado = false
            };

            Context.UnidadMedidas.Add(unidad);
            await Context.SaveChangesAsync();

            return unidad.Id;
        }

        public async Task Update(UnidadMedidaDto dto)
        {
            var unidad = await Context.UnidadMedidas.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (unidad == null) throw new ArgumentNullException();

            unidad.Descripcion = dto.Descripcion;
            unidad.Abreviatura = dto.Abreviatura;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var unidad = await Context.UnidadMedidas.FirstOrDefaultAsync(x => x.Id == id);

            if (unidad == null) throw new ArgumentNullException();

            unidad.Eliminado = !unidad.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<UnidadMedidaDto>> Get(string cadenaBuscar)
        {
            return await Context.UnidadMedidas.AsNoTracking()
                .Where(x => x.Descripcion.Contains(cadenaBuscar) || x.Abreviatura.Contains(cadenaBuscar)).Select(x =>
                    new UnidadMedidaDto()
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion,
                        Abreviatura = x.Abreviatura,
                        Eliminado = x.Eliminado
                    }).ToListAsync();
        }

        public async Task<UnidadMedidaDto> GetById(long id)
        {
            var unidad = await Context.UnidadMedidas
                .Include("MicroNutrienteDetalles")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (unidad == null) throw new ArgumentNullException();

            return new UnidadMedidaDto()
            {
                Id = unidad.Id,
                Codigo = unidad.Codigo,
                Descripcion = unidad.Descripcion,
                Abreviatura = unidad.Abreviatura,
                Eliminado = unidad.Eliminado,
                MicroNutrienteDetalles = unidad.MicroNutrienteDetalles.Select(x=> new MicroNutrienteDetalleDto()
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
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.UnidadMedidas.AnyAsync() ? await Context.UnidadMedidas.MaxAsync(x => x.Codigo) + 1 : 1;
        }
    }
}
