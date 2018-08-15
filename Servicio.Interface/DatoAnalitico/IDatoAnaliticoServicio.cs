using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.SubGrupo;

namespace Servicio.Interface.DatoAnalitico
{
    public interface IDatoAnaliticoServicio
    {
        Task<long> Add(DatoAnaliticoDto dto);
        Task Update(DatoAnaliticoDto dto);
        Task Delete(long id);
        Task<ICollection<DatoAnaliticoDto>> Get(string cadenaBuscar);
        Task<DatoAnaliticoDto> GetById(long id);
    }
}
