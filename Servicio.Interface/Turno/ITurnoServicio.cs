using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.SubGrupo;

namespace Servicio.Interface.Turno
{
    public interface ITurnoServicio
    {
        Task<long> Add(TurnoDto dto);
        Task Update(TurnoDto dto);
        Task Delete(long id);
        Task<ICollection<TurnoDto>> Get(bool eliminado, string cadenaBuscar);
        Task<TurnoDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
