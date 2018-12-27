using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.Opcion
{
    public class OpcionServicio : ServicioBase, IOpcionServicio
    {
        public async Task<long> Add(OpcionDto dto)
        {
            var opcion = new Dominio.Entidades.Opcion()
            {
                Codigo = dto.Codigo,
                ComidaId = dto.ComidaId,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Opciones.Add(opcion);
            await Context.SaveChangesAsync();
            return opcion.Id;
        }

        public async Task Update(OpcionDto dto)
        {
            var opcion = await Context.Opciones.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (opcion == null) throw new ArgumentNullException();

            opcion.ComidaId = dto.ComidaId;
            opcion.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var opcion = await Context.Opciones.FirstOrDefaultAsync(x => x.Id == id);

            if (opcion == null) throw new ArgumentNullException();

            opcion.Eliminado = !opcion.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<OpcionDto>> Get(string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);

            return await Context.Opciones
                .AsNoTracking()
                .Include("Comida")
                .Where(x => x.Codigo == codigo || x.Descripcion.Contains(cadenaBuscar))
                .Select(x => new OpcionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    ComidaId = x.ComidaId,
                    ComidaStr = x.Comida.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<OpcionDto> GetById(long id)
        {
            var opcion = await Context.Opciones
                .AsNoTracking()
                .Include("Comida")
                .Include("OpcionDetalles.Alimento")
                .Include("OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (opcion == null) throw new ArgumentNullException();

            return new OpcionDto()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                Descripcion = opcion.Descripcion,
                ComidaId = opcion.ComidaId,
                ComidaStr = opcion.Comida.Descripcion,
                Eliminado = opcion.Eliminado,
                OpcionDetalles = opcion.OpcionDetalles.Select(x=> new OpcionDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    Cantidad = x.Cantidad,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.Opcion.Descripcion,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedida.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Opciones.AnyAsync()
                ? await Context.Opciones.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
