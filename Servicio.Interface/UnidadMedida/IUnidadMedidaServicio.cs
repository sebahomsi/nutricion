using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.UnidadMedida
{
    public interface IUnidadMedidaServicio
    {
        Task<long> Add(UnidadMedidaDto dto);
        Task Update(UnidadMedidaDto dto);
        Task Delete(long id);
        Task<ICollection<UnidadMedidaDto>> Get(string cadenaBuscar);
        Task<UnidadMedidaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
