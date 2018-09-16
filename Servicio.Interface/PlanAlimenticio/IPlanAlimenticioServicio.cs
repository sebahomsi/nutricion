using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Patologia;

namespace Servicio.Interface.PlanAlimenticio
{
    public interface IPlanAlimenticioServicio
    {
        Task<long> Add(PlanAlimenticioDto dto);
        Task Update(PlanAlimenticioDto dto);
        Task Delete(long id);
        Task<ICollection<PlanAlimenticioDto>> Get(string cadenaBuscar);
        Task<PlanAlimenticioDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
