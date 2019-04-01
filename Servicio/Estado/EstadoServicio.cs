using AutoMapper;
using Servicio.Interface.Estado;
using Servicio.Interface.Estado.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio.Estado
{
    public class EstadoServicio : ServicioBase, IEstadoServicio
    {
        public async Task<long> Add(EstadoDto dto)
        {
            var estado = new Dominio.Entidades.Estado()
            {
                Codigo = dto.Codigo,
                Color = dto.Color,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.Estados.Add(estado);
            await Context.SaveChangesAsync();
            return estado.Id;
        }

        public async Task Delete(long id)
        {
            var estado = await Context.Estados.FirstOrDefaultAsync(x => x.Id == id);

            if(estado == null) throw new ArgumentNullException($"No se encontro el Estado");

            estado.Eliminado = !estado.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<EstadoDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            var estados = await Context.Estados.Where(x => x.Descripcion.Contains(cadenaBuscar) && x.Eliminado == eliminado).ToListAsync();

            var estadosDto = Mapper.Map<ICollection<EstadoDto>>(estados);

            return estadosDto;
        }

        public async Task<EstadoDto> GetById(long id)
        {
            var estado = await Context.Estados.FirstOrDefaultAsync(x => x.Id == id);

            if (estado == null) throw new ArgumentNullException($"No se encontro el Estado");

            var estadoDto = Mapper.Map<EstadoDto>(estado);

            return estadoDto;
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Estados.AnyAsync() ? await Context.Estados.MaxAsync(x => x.Codigo) + 1 : 1;
        }

        public async Task Update(EstadoDto dto)
        {
            var estado = await Context.Estados.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (estado == null) throw new ArgumentNullException($"No se encontro el Estado");

            estado.Descripcion = dto.Descripcion;
            estado.Color = dto.Color;

            await Context.SaveChangesAsync();
        }
    }
}
