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
        Task<ICollection<AlergiaIntoleranciaDto>> Get(string cadenaBuscar);
        Task<AlergiaIntoleranciaDto> GetById(long id);
    }
}
