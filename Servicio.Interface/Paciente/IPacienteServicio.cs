using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Paciente
{
    public interface IPacienteServicio
    {
        Task<long> Add(PacienteDto dto);
        Task Update(PacienteDto dto);
        Task Delete(long id);
        Task<ICollection<PacienteDto>> Get(long? establecimientoId,bool eliminado, string cadenaBuscar);
        Task<PacienteDto> GetById(long id);
        Task<PacienteDto> GetByEmail(string email);
        Task<int> GetNextCode();
    }
}
