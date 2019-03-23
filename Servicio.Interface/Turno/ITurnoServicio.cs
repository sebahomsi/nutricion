using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Turno
{
    public interface ITurnoServicio
    {
        Task<long> Add(TurnoDto dto);
        Task Update(TurnoDto dto);
        Task Delete(long id);
        Task<ICollection<TurnoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<TurnoDto> GetById(long id);
        Task<int> GetNextCode();
        Task<IEnumerable<TurnoDto>> GetByIdPaciente(long id);
    }
}
