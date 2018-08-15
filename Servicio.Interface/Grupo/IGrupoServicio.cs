using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Grupo
{
    public interface IGrupoServicio
    {
        Task<long> Add(GrupoDto dto);
        Task Update(GrupoDto dto);
        Task Delete(long id);
        Task<ICollection<GrupoDto>> Get(string cadenaBuscar);
        Task<GrupoDto> GetById(long id);
    }
}
