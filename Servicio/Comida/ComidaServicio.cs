using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Comida;
using Servicio.Interface.Opcion;

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
                Opciones = new List<Dominio.Entidades.Opcion>(),
                DiaId = diaId
            };
            Context.Comidas.Add(desayuno);

            var mediaMa = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 1,
                Descripcion = @"Media Mañana",
                Opciones = new List<Dominio.Entidades.Opcion>(),
                DiaId = diaId
            };
            Context.Comidas.Add(mediaMa);

            var almuerzo = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 2,
                Descripcion = @"Almuerzo",
                Opciones = new List<Dominio.Entidades.Opcion>(),
                DiaId = diaId
            };
            Context.Comidas.Add(almuerzo);

            var merienda = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 3,
                Descripcion = @"Merienda",
                Opciones = new List<Dominio.Entidades.Opcion>(),
                DiaId = diaId
            };
            Context.Comidas.Add(merienda);

            var mediaTa = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 4,
                Descripcion = @"Media Tarde",
                Opciones = new List<Dominio.Entidades.Opcion>(),
                DiaId = diaId
            };
            Context.Comidas.Add(mediaTa);

            var cena = new Dominio.Entidades.Comida()
            {
                Codigo = codigo + 5,
                Descripcion = @"Cena",
                Opciones = new List<Dominio.Entidades.Opcion>(),
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
                .Include("Opciones.OpcionDetalles")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (comida == null) throw new ArgumentNullException();

            return new ComidaDto()
            {
                Id = comida.Id,
                Codigo = comida.Codigo,
                Descripcion = comida.Descripcion,
                DiaId = comida.DiaId,
                DiaStr = comida.Dia.Descripcion,
                Opciones = comida.Opciones.Select(x => new OpcionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    ComidaId = x.ComidaId,
                    ComidaStr = x.Comida.Descripcion
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
