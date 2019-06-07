using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.SubGrupoReceta
{
    public interface ISubGrupoRecetaServicio
    {
        Task<long> Add(SubGrupoRecetaDto dto);
        Task Update(SubGrupoRecetaDto dto);
        Task Delete(long id);
        Task<ICollection<SubGrupoRecetaDto>> Get(bool eliminado, string cadenaBuscar);
        Task<SubGrupoRecetaDto> GetById(long id);
        Task<int> GetNextCode();
        Task QuitarRelacion(long? opcionId, long? subGrupoId);
        Task<ICollection<SubGrupoRecetaDto>> GetNotInOpcion(long? opcionId);
    }
}
