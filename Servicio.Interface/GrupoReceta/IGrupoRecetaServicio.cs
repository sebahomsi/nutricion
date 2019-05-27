using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.GrupoReceta
{
    public interface IGrupoRecetaServicio
    {
        Task<long> Add(GrupoRecetaDto dto);
        Task Update(GrupoRecetaDto dto);
        Task Delete(long id);
        Task<ICollection<GrupoRecetaDto>> Get(bool eliminado, string cadenaBuscar);
        Task<GrupoRecetaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
