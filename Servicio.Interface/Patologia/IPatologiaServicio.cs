using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.Interface.Patologia
{
    public interface IPatologiaServicio
    {
        Task<long> Add(PatologiaDto dto);
        Task Update(PatologiaDto dto);
        Task Delete(long id);
        Task<ICollection<PatologiaDto>> Get(string cadenaBuscar);
        Task<PatologiaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
