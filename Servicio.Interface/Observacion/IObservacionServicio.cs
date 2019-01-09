using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutrienteDetalle;

namespace Servicio.Interface.Observacion
{
    public interface IObservacionServicio
    {
        Task<long> Add(ObservacionDto dto);
        Task Update(ObservacionDto dto);
        Task Delete(long id);
        Task<ICollection<ObservacionDto>> Get(string cadenaBuscar);
        Task<ObservacionDto> GetById(long id);
        Task<ObservacionDto> GetByPacienteId(long id);
        Task<int> GetNextCode();
    }
}
