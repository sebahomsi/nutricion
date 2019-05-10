using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Servicio.Interface.ComidaDetalle;

namespace Servicio.ComidaDetalle
{
    public class ComidaDetalleServicio: ServicioBase, IComidaDetalleServicio
    {
        public async Task<long> Add(ComidaDetalleDto dto)
        {

            var verify = await VerifyDuplicity(dto);

            if (verify.HasValue)
            {
                await Activar(dto.Id);
                
                return dto.Id;
                //throw new ArgumentException("Ya existe una opcion igual");
            }
            var detalle = new Dominio.Entidades.ComidaDetalle()
            {
                Codigo = dto.Codigo,
                Comentario = dto.Comentario,
                OpcionId = dto.OpcionId,
                ComidaId = dto.ComidaId,
                Eliminado = false
            };

            Context.ComidasDetalles.Add(detalle);
            await Context.SaveChangesAsync();

            return detalle.Id;
        }

        private async Task Activar(long id)
        {
            Context.ComidasDetalles.FirstOrDefault(c => c.Id == id).Eliminado = false;
            await Context.SaveChangesAsync();
        }

        public async Task Update(ComidaDetalleDto dto)
        {
            var detalle = await Context.ComidasDetalles.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (detalle == null) throw new ArgumentNullException();

            detalle.Comentario = dto.Comentario;
            detalle.OpcionId = dto.OpcionId;
            detalle.ComidaId = dto.ComidaId;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var detalle = await Context.ComidasDetalles.FirstOrDefaultAsync(x => x.Id == id);

            if (detalle == null) throw new ArgumentNullException();

            detalle.Eliminado = !detalle.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<ComidaDetalleDto>> Get(bool eliminado, string cadenaBuscar)
        {
            Expression<Func<Dominio.Entidades.ComidaDetalle, bool>> expression = x => x.Eliminado == eliminado && x.Comentario.Contains(cadenaBuscar);

            return await Context.ComidasDetalles
                .AsNoTracking()
                .Include("Comida")
                .Include("Opcion")
                .Where(expression).Select(x => new ComidaDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Comentario = x.Comentario,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.Opcion.Descripcion,
                    ComidaId = x.ComidaId,
                    ComidaStr = x.Comida.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<ComidaDetalleDto> GetById(long id)
        {
            var detalle = await Context.ComidasDetalles
                .AsNoTracking()
                .Include("Comida")
                .Include("Opcion")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (detalle == null) throw new ArgumentNullException();

            return new ComidaDetalleDto()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                Comentario = detalle.Comentario,
                ComidaId = detalle.ComidaId,
                ComidaStr = detalle.Comida.Descripcion,
                OpcionId = detalle.OpcionId,
                OpcionStr = detalle.Opcion.Descripcion,
                Eliminado = detalle.Eliminado
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.ComidasDetalles.AnyAsync()
                ? await Context.ComidasDetalles.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<long?> VerifyDuplicity(ComidaDetalleDto dto)
        {
            var id = await Context.ComidasDetalles
                .FirstOrDefaultAsync(x => x.ComidaId == dto.ComidaId&&x.OpcionId==dto.OpcionId);

            return id?.Id;
        }
    }
}
