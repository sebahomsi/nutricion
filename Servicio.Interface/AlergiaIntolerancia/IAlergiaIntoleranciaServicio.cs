using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Empleado;

namespace Servicio.Interface.AlergiaIntolerancia
{
    public interface IAlergiaIntoleranciaServicio
    {
        Task<long> Add(AlergiaIntoleranciaDto dto);
        Task Update(AlergiaIntoleranciaDto dto);
        Task Delete(long id);
        Task<ICollection<AlergiaIntoleranciaDto>> Get(bool eliminado, string cadenaBuscar);

        Task<ICollection<AlergiaIntoleranciaDto>> GetbyObservacionId(bool eliminado, string cadenaBuscar, long observacionId);
        Task<AlergiaIntoleranciaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
