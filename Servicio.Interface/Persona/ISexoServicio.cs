using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Persona
{
    public interface ISexoServicio
    {
        Task<IEnumerable<SexoDto>> ObtenerSexo();
    }
}
