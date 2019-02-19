using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MacroNutriente;

namespace Servicio.Interface.Paciente
{
    public interface IPacienteServicio
    {
        Task<long> Add(PacienteDto dto);
        Task Update(PacienteDto dto);
        Task Delete(long id);
        Task<ICollection<PacienteDto>> Get(bool eliminado, string cadenaBuscar);
        Task<PacienteDto> GetById(long id);
        Task<PacienteDto> GetByEmail(string email);
        Task<int> GetNextCode();
    }
}
