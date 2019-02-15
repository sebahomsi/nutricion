using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Mensaje;

namespace Servicio.Mensaje
{
    public class MensajeServicio : ServicioBase, IMensajeServicio
    {
        public async Task<long> Add(MensajeDto dto)
        {
            var mensaje = new Dominio.Entidades.Mensaje()
            {
                EmailEmisor = dto.EmailEmisor,
                EmailReceptor = dto.EmailReceptor,
                Cuerpo = dto.Cuerpo,
                Motivo = dto.Motivo,
                Visto = false
            };

            Context.Mensajes.Add(mensaje);
            await Context.SaveChangesAsync();

            return mensaje.Id;
        }

        public async Task ChangeVisto(long id)
        {
            var mensaje = await Context.Mensajes.FirstOrDefaultAsync(x => x.Id == id);

            if (mensaje == null) throw new ArgumentNullException();

            mensaje.Visto = true;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<MensajeDto>> GetRecibidos(string email)
        {
            return await Context.Mensajes
                .AsNoTracking()
                .Where(x => x.EmailReceptor == email).Select(x => new MensajeDto()
                {
                    Id = x.Id,
                    EmailReceptor = x.EmailReceptor,
                    EmailEmisor = x.EmailEmisor,
                    Cuerpo = x.Cuerpo,
                    Motivo = x.Motivo,
                    Visto = x.Visto
                }).ToListAsync();
        }

        public async Task<ICollection<MensajeDto>> GetEnviados(string email)
        {
            return await Context.Mensajes
                .AsNoTracking()
                .Where(x => x.EmailEmisor == email).Select(x => new MensajeDto()
                {
                    Id = x.Id,
                    EmailReceptor = x.EmailReceptor,
                    EmailEmisor = x.EmailEmisor,
                    Motivo = x.Motivo,
                    Cuerpo = x.Cuerpo,
                    Visto = x.Visto
                }).ToListAsync();
        }

        public async Task<MensajeDto> GetById(long id)
        {
            var mensaje = await Context.Mensajes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (mensaje == null) throw new ArgumentNullException();

            return new MensajeDto()
            {
                Id = mensaje.Id,
                EmailReceptor = mensaje.EmailReceptor,
                EmailEmisor = mensaje.EmailEmisor,
                Motivo = mensaje.Motivo,
                Cuerpo = mensaje.Cuerpo,
                Visto = mensaje.Visto
            };
        }
    }
}
