using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAnalitico;

namespace Servicio.Interface.MacroNutriente
{
    public interface IMacroNutrienteServicio
    {
        Task<long> Add(MacroNutrienteDto dto);
        Task Update(MacroNutrienteDto dto);
        Task Delete(long id);
        Task<ICollection<MacroNutrienteDto>> Get(string cadenaBuscar);
        Task<MacroNutrienteDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
