using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Comida
{
    public interface IComidaServicio
    {
        Task<long> Add(ComidaDto dto);
        Task Update(ComidaDto dto);
        Task Delete(long id);
        Task<ICollection<ComidaDto>> Get(string cadenaBuscar);
        Task<ComidaDto> GetById(long id);
        Task<int> GetNextCode();
        Task GenerarComidas(long diaId);
    }
}
