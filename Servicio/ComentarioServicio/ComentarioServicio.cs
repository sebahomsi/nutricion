using Servicio.Interface.Comentario;
using Servicio.Interface.Opcion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.ComentarioServicio
{
    public class ComentarioServicio : ServicioBase, IComentarioServicio
    {
        public async Task Add(List<OpcionDto> opciones,ComentarioDto dto)
        {
            var comentario = new Dominio.Entidades.Comentario()
            {
                ComidaId = dto.ComidaId,
                DiaId = dto.DiaId,
                PlanId = dto.PlanId,
                Texto = dto.Texto,
            };
            Context.Comentarios.Add(comentario);
            await Context.SaveChangesAsync();
            if (opciones.Count() > 0)
            {
                foreach (var opcion in opciones)
                {
                    var op = Context.Opciones.FirstOrDefault(x => x.Id == opcion.Id);
                    op.ComentarioId = comentario.Id;
                    await Context.SaveChangesAsync();
                }
            }
        }

        public async Task Delete(long id)
        {
            var delete = Context.Comentarios.FirstOrDefault(x => x.Id == id);
            Context.Comentarios.Remove(delete);
            await Context.SaveChangesAsync();
        }

        public async Task<ComentarioDto> GetById(long Id)
        {
            var dato = await Context.Comentarios.Include(x=>x.Opciones).FirstOrDefaultAsync(x => x.Id == Id);
            var dto = new ComentarioDto()
            {
                Id= dato.Id,
                ComidaId=dato.ComidaId,
                DiaId=dato.DiaId,
                PlanId=dato.PlanId,
                Texto=dato.Texto,
            };
            return dto;
        }

        public async Task<ComentarioDto> GetSpecific(long planId, long diaId, long comidaId, long opcionId)
        {
            var dato = await Context.Comentarios.Include(x => x.Opciones)
                .FirstOrDefaultAsync(x => x.PlanId == planId && x.DiaId == diaId && x.Opciones.Any(c => c.Id == opcionId));
            var dto = new ComentarioDto()
            {
                Id = dato.Id,
                ComidaId = dato.ComidaId,
                DiaId = dato.DiaId,
                PlanId = dato.PlanId,
                Texto = dato.Texto,
            };
            return dto;
        }

        public async Task Update(ComentarioDto dto)
        {
            var dato = await Context.Comentarios.FirstOrDefaultAsync(x => x.Id == dto.Id);
            dato.Texto = dto.Texto;
            await Context.SaveChangesAsync();
        }
    }
}
