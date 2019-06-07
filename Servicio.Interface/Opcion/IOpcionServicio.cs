using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Opcion
{
    public interface IOpcionServicio
    {
        Task<long> Add(OpcionDto dto,long? subGrupoId);
        Task Update(OpcionDto dto);
        Task Delete(long id);
        Task<ICollection<OpcionDto>> Get(bool eliminado, long? idSub, string cadenaBuscar);
        Task<OpcionDto> GetById(long id);
        Task<int> GetNextCode();
        Task<List<OpcionDto>> FindRecipeByFoods(List<long> alimentosIds);
    }
}
