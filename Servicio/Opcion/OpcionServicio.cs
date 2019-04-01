using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.Opcion
{
    public class OpcionServicio : ServicioBase, IOpcionServicio
    {
        public async Task<long> Add(OpcionDto dto)
        {
            var verify = await VerifyDuplicity(dto);
            if (verify.HasValue) throw new ArgumentException("Ya existe una opcion con esa Descripcion");
            var opcion = new Dominio.Entidades.Opcion()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false,               

            };

            Context.Opciones.Add(opcion);
            await Context.SaveChangesAsync();
            return opcion.Id;
        }

        public async Task Update(OpcionDto dto)
        {
            var opcion = await Context.Opciones.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (opcion == null) throw new ArgumentNullException();

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

        public async Task<ICollection<OpcionDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Opcion, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            return await Context.Opciones
                .AsNoTracking()
                .Where(expression)
                .Select(x => new OpcionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<OpcionDto> GetById(long id)
        {
            var opcion = await Context.Opciones
                .AsNoTracking()
                .Include("ComidasDetalles.Comida")
                .Include("OpcionDetalles.Alimento")
                .Include("OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (opcion == null) throw new ArgumentNullException();

            return new OpcionDto()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                Descripcion = opcion.Descripcion,
                Eliminado = opcion.Eliminado,
                OpcionDetalles = opcion.OpcionDetalles.Where(x=> x.Eliminado == false).Select(x=> new OpcionDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.Alimento.Descripcion,
                    Cantidad = x.Cantidad,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.Opcion.Descripcion,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedida.Abreviatura,
                    Eliminado = x.Eliminado
                }).ToList(),
                ComidasDetalles = opcion.ComidasDetalles.Select(t => new ComidaDetalleDto()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Comentario = t.Comentario,
                    ComidaId = t.ComidaId,
                    OpcionId = t.OpcionId,
                    OpcionStr = t.Opcion.Descripcion,
                    ComidaStr = t.Comida.Descripcion,
                    Eliminado = t.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Opciones.AnyAsync()
                ? await Context.Opciones.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<long?> VerifyDuplicity(OpcionDto dto)
        {
            var id = await Context.Opciones
                .FirstOrDefaultAsync(x => x.Descripcion == dto.Descripcion);

            return id?.Id;
        }
    }
}
