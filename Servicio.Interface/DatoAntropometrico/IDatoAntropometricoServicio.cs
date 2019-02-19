using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Comida;

namespace Servicio.Interface.DatoAntropometrico
{
    public interface IDatoAntropometricoServicio
    {
        Task<long> Add(DatoAntropometricoDto dto);
        Task Update(DatoAntropometricoDto dto);
        Task Delete(long id);
        Task<ICollection<DatoAntropometricoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<DatoAntropometricoDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
