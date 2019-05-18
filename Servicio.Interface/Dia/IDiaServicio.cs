using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAntropometrico;

namespace Servicio.Interface.Dia
{
    public interface IDiaServicio
    {
        Task GenerarDias(long planId);
        Task<DiaDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
