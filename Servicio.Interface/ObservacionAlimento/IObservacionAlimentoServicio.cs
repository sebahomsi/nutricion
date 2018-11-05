using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.ObservacionAlimento
{
    public interface IObservacionAlimentoServicio
    {
        Task Add(long observacionId, long alimentoId);
        Task Delete(long observacionId, long alimentoId);
    }
}
