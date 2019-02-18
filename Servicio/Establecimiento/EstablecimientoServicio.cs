using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Establecimiento;

namespace Servicio.Establecimiento
{
    public class EstablecimientoServicio : ServicioBase, IEstablecimientoServicio
    {
        public async Task<long> Add(EstablecimientoDto dto)
        {
            var establecimiento = new Dominio.Entidades.Establecimiento()
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Email = dto.Email,
                Facebook = dto.Facebook,
                Instagram = dto.Instagram,
                Telefono = dto.Telefono,
                Profesional = dto.Profesional,
                Horario = dto.Horario,
                Twitter = dto.Twitter
            };
            Context.Establecimientos.Add(establecimiento);
            await Context.SaveChangesAsync();

            return establecimiento.Id;
        }

        public async Task Update(EstablecimientoDto dto)
        {
            var establecimiento = await Context.Establecimientos.FirstOrDefaultAsync(x => x.Id == dto.Id);

            establecimiento.Nombre = dto.Nombre;
            establecimiento.Direccion = dto.Direccion;
            establecimiento.Email = dto.Email;
            establecimiento.Facebook = dto.Facebook;
            establecimiento.Instagram = dto.Instagram;
            establecimiento.Telefono = dto.Telefono;
            establecimiento.Profesional = dto.Profesional;
            establecimiento.Horario = dto.Horario;
            establecimiento.Twitter = dto.Twitter;

            await Context.SaveChangesAsync();
        }

        public async Task<EstablecimientoDto> GetById(long id)
        {
            var establecimiento = await Context.Establecimientos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (establecimiento == null) return null;

            return new EstablecimientoDto()
            {
                Id = establecimiento.Id,
                Nombre = establecimiento.Nombre,
                Direccion = establecimiento.Direccion,
                Profesional = establecimiento.Profesional,
                Email = establecimiento.Email,
                Facebook = establecimiento.Facebook,
                Horario = establecimiento.Horario,
                Instagram = establecimiento.Instagram,
                Telefono = establecimiento.Telefono,
                Twitter = establecimiento.Twitter
            };
        }

        public async Task<ICollection<EstablecimientoDto>> Get()
        {
            return await Context.Establecimientos
                .AsNoTracking()
                .Select(x => new EstablecimientoDto()
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Direccion = x.Direccion,
                    Email = x.Email,
                    Facebook = x.Facebook,
                    Instagram = x.Instagram,
                    Telefono = x.Telefono,
                    Profesional = x.Profesional,
                    Horario = x.Horario,
                    Twitter = x.Twitter
                }).ToListAsync();
        }

        public async Task<bool> EstablecimientoFlag()
        {
            return await Context.Establecimientos.AnyAsync();

        }
    }
}
