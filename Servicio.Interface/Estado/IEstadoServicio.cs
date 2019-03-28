using Servicio.Interface.Estado.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Estado
{
    public interface IEstadoServicio
    {
        Task<long> Add(EstadoDto dto);

        Task Update(EstadoDto dto);

        Task Delete(long id);

        Task<ICollection<EstadoDto>> Get(bool eliminado, string cadenaBuscar);

        Task<EstadoDto> GetById(long id);

        Task<int> GetNextCode();
    }
}
