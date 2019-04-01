using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.MacroNutriente
{
    public interface IMacroNutrienteServicio
    {
        Task<long> Add(MacroNutrienteDto dto);
        Task Update(MacroNutrienteDto dto);
        Task Delete(long id);
        Task<ICollection<MacroNutrienteDto>> Get(bool eliminado, string cadenaBuscar);
        Task<MacroNutrienteDto> GetById(long id);
    }
}
