using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Pago
{
   public interface IPagoServicio
    {
        Task<long> Add(PagoDto dto);
        Task Update(PagoDto dto);
        Task Delete(long id);
        Task<ICollection<PagoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<ICollection<PagoDto>> GetByDate(DateTime fecha, bool eliminado, string cadenaBuscar);
        Task<PagoDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
