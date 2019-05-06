using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Estrategia
{
    public interface IEstrategiaServicio
    {
        Task Add(EstrategiaDto dto);
        Task Update(EstrategiaDto dto);
        Task<EstrategiaDto> GetByPacienteId(long id);
    }
}
