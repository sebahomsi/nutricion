using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Mensaje
{
    public interface IMensajeServicio
    {
        Task<long> Add(MensajeDto dto);
        Task ChangeVisto(long id);
        Task<ICollection<MensajeDto>> GetRecibidos(string email);
        Task<ICollection<MensajeDto>> GetEnviados(string email);
        Task<MensajeDto> GetById(long id);
    }
}
