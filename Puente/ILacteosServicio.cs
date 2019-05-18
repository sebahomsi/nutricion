
using System;
using System.Collections.Generic;
using System.Text;

namespace Puente
{
    public interface ILacteosServicio
    {
        IList<AlimentoDto> ListarLacteos();
    }
}
