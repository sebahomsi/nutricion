using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Patologia
{
    public interface IPatologiaServicio
    {
        Task<long> Add(PatologiaDto dto);
        Task Update(PatologiaDto dto);
        Task Delete(long id);
        Task<ICollection<PatologiaDto>> Get(bool eliminado, string cadenaBuscar);
        Task<ICollection<PatologiaDto>> GetbyObservacionId(bool eliminado, string cadenaBuscar, long observacionId);
        Task<PatologiaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
