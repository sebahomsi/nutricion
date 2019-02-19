using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Paciente;

namespace Servicio.Interface.Empleado
{
    public interface IEmpleadoServicio
    {
        Task<long> Add(EmpleadoDto dto);
        Task Update(EmpleadoDto dto);
        Task Delete(long id);
        Task<ICollection<EmpleadoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<EmpleadoDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
