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
using Servicio.Interface.SubGrupoReceta;

namespace Servicio.Opcion
{
    public class OpcionServicio : ServicioBase, IOpcionServicio
    {
        public async Task<long> Add(OpcionDto dto, IEnumerable<long?> subGruposId)
        {
            var verify = await VerifyDuplicity(dto);
            if (verify.HasValue) throw new ArgumentException("Ya existe una opcion con esa Descripcion");
            var opcion = new Dominio.Entidades.Opcion()
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false,             

            };
            if (subGruposId.Any())
            {
                foreach (var id in subGruposId)
                {
                    var subGrupo = await Context.SubGruposRecetas.Include(x=>x.Opciones).FirstOrDefaultAsync(x => x.Id == id);
                    subGrupo?.Opciones.Add(opcion);
                }
            }
            else
            {
                Context.Opciones.Add(opcion);
            }
            await Context.SaveChangesAsync();
            return opcion.Id;
        }

        public async Task Update(OpcionDto dto, IEnumerable<long?> subGruposId)
        {
            var opcion = await Context.Opciones.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (opcion == null) throw new ArgumentNullException();

            opcion.Descripcion = dto.Descripcion;

            if (subGruposId.Any())
            {
                foreach (var id in subGruposId)
                {
                    var subGrupo = await Context.SubGruposRecetas.Include(x => x.Opciones).FirstOrDefaultAsync(x => x.Id == id);
                    subGrupo?.Opciones.Add(opcion);
                }
            }

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var opcion = await Context.Opciones.FirstOrDefaultAsync(x => x.Id == id);

            if (opcion == null) throw new ArgumentNullException();

            opcion.Eliminado = !opcion.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<OpcionDto>> Get(bool eliminado, long? idSub, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Opcion, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);

            var lista= await Context.Opciones
                .AsNoTracking()
                .Include(p=>p.SubGruposRecetas)
                .Where(expression)
                .Select(x => new OpcionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado,
                    SubGruposRecetas = x.SubGruposRecetas.Select( s =>new SubGrupoRecetaDto() {
                       Id = s.Id,
                       Codigo = s.Codigo,
                       Descripcion = s.Descripcion,
                       Eliminado = s.Eliminado     
                    }).ToList()
                }).ToListAsync();

            return idSub.HasValue ? lista.Where(x => x.SubGruposRecetas.Any(s => s.Id == idSub.Value)).ToList() : lista;
        }

        public async Task<OpcionDto> GetById(long id)
        {
            var opcion = await Context.Opciones
                .AsNoTracking()
                .Include("ComidasDetalles.Comida")
                .Include("OpcionDetalles.Alimento")
                .Include("OpcionDetalles.UnidadMedida")
                .Include(x=>x.SubGruposRecetas)
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
                }).ToList(),
                SubGruposRecetas = opcion.SubGruposRecetas.Select(s => new SubGrupoRecetaDto()
                {
                    Id = s.Id,
                    Codigo = s.Codigo,
                    Descripcion = s.Descripcion,
                    Eliminado = s.Eliminado
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

        public async Task<List<OpcionDto>> FindRecipeByFoods(List<long> alimentosIds)
        {
            var recetasFiltradas = new List<OpcionDto>();
            var flag = false;
            var recetas = await Context.Opciones
                .AsNoTracking()
                .Include("OpcionesDetalles")
                .Select(opcion => new OpcionDto()
                {
                    Id = opcion.Id,
                    Codigo = opcion.Codigo,
                    Descripcion = opcion.Descripcion,
                    Eliminado = opcion.Eliminado,
                    OpcionDetalles = opcion.OpcionDetalles.Where(x => x.Eliminado == false).Select(x => new OpcionDetalleDto()
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
                    }).ToList()
                }).ToListAsync();

            foreach (var receta in recetas)
            {
                foreach (var detalle in receta.OpcionDetalles)
                {
                    foreach (var alimento in alimentosIds)
                    {
                        if (detalle.AlimentoId == alimento)
                        {
                            flag = true;
                        }
                    }
                }
                if (flag == true)
                {
                    recetasFiltradas.Add(receta);
                    flag = false;
                }
            }
            return recetasFiltradas;
        }
    }
}
