using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.ObservacionPatologia
{
    public interface IObservacionPatologiaServicio
    {
        Task Add(long observacionId, long patologiaId);
        Task Delete(long observacionId, long patologiaId);
    }
}
