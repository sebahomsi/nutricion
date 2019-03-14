using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.RecetaDetalle
{
    public interface IRecetaDetalleServicio
    {
        Task<long> Add(RecetaDetalleDto dto);
        Task Update(RecetaDetalleDto dto);
        Task Delete(long id);
        Task<ICollection<RecetaDetalleDto>> Get(bool eliminado, string cadenaBuscar);
        Task<RecetaDetalleDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
