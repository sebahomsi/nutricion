using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAntropometrico;

namespace Servicio.Interface.MicroNutriente
{
    public interface IMicroNutrienteServicio
    {
        Task<long> Add(MicroNutrienteDto dto);
        Task Update(MicroNutrienteDto dto);
        Task Delete(long id);
        Task<ICollection<MicroNutrienteDto>> Get(bool eliminado, string cadenaBuscar);
        Task<MicroNutrienteDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
