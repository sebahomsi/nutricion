using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Establecimiento
{
    public interface IEstablecimientoServicio
    {
        Task<long> Add(EstablecimientoDto dto);
        Task Update(EstablecimientoDto dto);
        Task<EstablecimientoDto> GetById(long id);
    }
}
