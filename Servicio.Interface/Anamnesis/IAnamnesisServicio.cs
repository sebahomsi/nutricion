using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Anamnesis
{
    public interface IAnamnesisServicio
    {
        Task Add(AnamnesisDto dto);
        Task Update(AnamnesisDto dto);
        Task<AnamnesisDto> GetByPacienteId(long id);
    }
}
