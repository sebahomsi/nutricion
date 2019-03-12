using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.RecetaAlimento
{
    public interface IRecetaAlimentoServicio
    {
        Task Add(long recetaId, long alimentoId);
        Task Delete(long recetaId, long alimentoId);
    }
}
