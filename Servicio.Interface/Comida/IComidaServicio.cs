using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;

namespace Servicio.Interface.Comida
{
    public interface IComidaServicio
    {
        Task<long> Add(ComidaDto dto);
        Task Update(ComidaDto dto);
        Task Delete(long id);
        Task<ICollection<ComidaDto>> Get(string cadenaBuscar);
        Task<ComidaDto> GetById(long id);
    }
}
