using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.DatoAnalitico
{
    public interface IDatoAnaliticoServicio
    {
        Task<long> Add(DatoAnaliticoDto dto);
        Task Update(DatoAnaliticoDto dto);
        Task Delete(long id);
        Task<ICollection<DatoAnaliticoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<DatoAnaliticoDto> GetById(long id);
        Task<int> GetNextCode();
        Task<IEnumerable<DatoAnaliticoDto>> GetByIdPaciente(long id);
    }
}
