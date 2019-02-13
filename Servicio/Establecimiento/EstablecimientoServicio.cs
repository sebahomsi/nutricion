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

            if (establecimiento == null) throw new ArgumentNullException();

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
    }
}
