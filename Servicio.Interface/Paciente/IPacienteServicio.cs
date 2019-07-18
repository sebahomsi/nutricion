using System;
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
        Task<IEnumerable<PacienteDto>> GetByDateNewPacientes(DateTime desde, DateTime hasta);

        Task<IEnumerable<PacienteDto>> GetByDateActivePacientes(DateTime desde, DateTime hasta);
        Task<IEnumerable<PacienteDto>> GetByDateInactivePacientes(DateTime desde, DateTime hasta);
        Task<PacienteDto> GetById(long id);
        Task<PacienteDto> GetByEmail(string email);
        Task<int> GetNextCode();
    }
}
