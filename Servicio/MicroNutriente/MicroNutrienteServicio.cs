using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.MicroNutrienteDetalle;

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
            var micro = await Context.MicroNutrientes.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (micro == null) throw new ArgumentNullException();

            micro.Descripcion = dto.Descripcion;
            
            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var micro = await Context.MicroNutrientes.FirstOrDefaultAsync(x => x.Id == id);
            if (micro == null) throw new ArgumentNullException();

            micro.Eliminado = !micro.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MicroNutrienteDto>> Get(string cadenaBuscar = "")
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
                .Include("MicroNutrienteDetalles")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (micro == null) throw new ArgumentNullException();

            return new MicroNutrienteDto()
            {
                Id = micro.Id,
                Codigo = micro.Codigo,
                Descripcion = micro.Descripcion,
                Eliminado = micro.Eliminado,
                MicroNutrienteDetalles = micro.MicroNutrienteDetalles.Select(x=> new MicroNutrienteDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    MicroNutrienteId = x.MicroNutrienteId,
                    UnidadMedidaId = x.UnidadMedidaId,
                    Cantidad = x.Cantidad
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.MicroNutrientes.AnyAsync()
                ? await Context.MicroNutrientes.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
