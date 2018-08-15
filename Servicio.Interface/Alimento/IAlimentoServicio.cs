using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.AlergiaIntolerancia;

namespace Servicio.Interface.Alimento
{
    public interface IAlimentoServicio
    {
        Task<long> Add(AlimentoDto dto);
        Task Update(AlimentoDto dto);
        Task Delete(long id);
        Task<ICollection<AlimentoDto>> Get(string cadenaBuscar);
        Task<AlimentoDto> GetById(long id);
    }
}
