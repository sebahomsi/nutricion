using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.Comida
{
    public interface IComidaServicio
    {
        Task<ComidaDto> GetById(long id);
        Task<int> GetNextCode();
        Task GenerarComidas(long diaId);
    }
}
