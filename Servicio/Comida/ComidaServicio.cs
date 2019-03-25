using Servicio.Interface.Comida;
using Servicio.Interface.Opcion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Servicio.Interface.ComidaDetalle;

namespace Servicio.Comida
{
    public class ComidaServicio : ServicioBase, IComidaServicio
    {

        public async Task GenerarComidas(long diaId)
        {
            var codigo = await GetNextCode();
            var desayuno = new Dominio.Entidades.Comida()
            {
                Codigo = codigo,
                Descripcion = @"Desayuno",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(desayuno);

            var mediaMa = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 1,
                Descripcion = @"Media Mañana",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(mediaMa);

            var almuerzo = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 2,
                Descripcion = @"Almuerzo",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(almuerzo);

            var merienda = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 3,
                Descripcion = @"Merienda",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(merienda);

            var mediaTa = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 4,
                Descripcion = @"Media Tarde",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(mediaTa);

            var cena = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 5,
                Descripcion = @"Cena",
                ComidasDetalles = new List<Dominio.Entidades.ComidaDetalle>(),
                DiaId = diaId
            };
            Context.Comidas.Add(cena);


            await Context.SaveChangesAsync();
        }

        public async Task<long> Add(ComidaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ComidaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ComidaDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<ComidaDto> GetById(long id)
        {
            var comida = await Context.Comidas
                .AsNoTracking()
                .Include("Dia")
                .Include("ComidasDetalles.Opcion")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (comida == null) throw new ArgumentNullException();

            return new ComidaDto()
            {
                Id = comida.Id,
                Codigo = comida.Codigo,
                Descripcion = comida.Descripcion,
                DiaId = comida.DiaId,
                DiaStr = comida.Dia.Descripcion,
                ComidasDetalles = comida.ComidasDetalles.Select(x => new ComidaDetalleDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Comentario = x.Comentario,
                    ComidaId = x.ComidaId,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.Opcion.Descripcion,
                    ComidaStr = x.Comida.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Comidas.AnyAsync()
                ? await Context.Comidas.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
