using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Opcion;

namespace Servicio.Interface.OpcionDetalle
{
    public interface IOpcionDetalleServicio
    {
        Task<long> Add(OpcionDetalleDto dto);
        Task Update(OpcionDetalleDto dto);
        Task Delete(long id);
        Task<ICollection<OpcionDetalleDto>> Get(string cadenaBuscar);
        Task<OpcionDetalleDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
