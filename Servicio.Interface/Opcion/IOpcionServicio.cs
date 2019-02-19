using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Observacion;

namespace Servicio.Interface.Opcion
{
    public interface IOpcionServicio
    {
        Task<long> Add(OpcionDto dto);
        Task Update(OpcionDto dto);
        Task Delete(long id);
        Task<ICollection<OpcionDto>> Get(bool eliminado, string cadenaBuscar);
        Task<OpcionDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
