using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Objetivo
{
    public interface IObjetivoServicio
    {
        Task Add(ObjetivoDto dto);
        Task Update(ObjetivoDto dto);
        Task<ObjetivoDto> GetByPacienteId(long id);
    }
}
