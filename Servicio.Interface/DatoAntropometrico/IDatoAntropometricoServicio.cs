using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.DatoAntropometrico
{
    public interface IDatoAntropometricoServicio
    {
        Task<long> Add(DatoAntropometricoDto dto);
        Task Update(DatoAntropometricoDto dto);
        Task Delete(long id);
        Task<ICollection<DatoAntropometricoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<DatoAntropometricoDto> GetById(long id);
        Task<int> GetNextCode();
        Task<IEnumerable<DatoAntropometricoDto>> GetByIdPaciente(long id);
    }
}
