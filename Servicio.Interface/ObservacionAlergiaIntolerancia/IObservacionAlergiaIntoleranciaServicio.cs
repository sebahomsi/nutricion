using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.ObservacionAlergiaIntolerancia
{
    public interface IObservacionAlergiaIntoleranciaServicio
    {
        Task Add(long observacionId, long alergiaId);
        Task Delete(long observacionId, long alergiaId);
    }
}
