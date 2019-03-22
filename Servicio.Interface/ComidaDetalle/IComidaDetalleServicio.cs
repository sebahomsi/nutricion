using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.ComidaDetalle
{
    public interface IComidaDetalleServicio
    {
        Task<long> Add(ComidaDetalleDto dto);
        Task Update(ComidaDetalleDto dto);
        Task Delete(long id);
        Task<ICollection<ComidaDetalleDto>> Get(bool eliminado, string cadenaBuscar);
        Task<ComidaDetalleDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
