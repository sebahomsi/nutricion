using Servicio.Interface.Comida;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servicio.Interface.PlanAlimenticio
{
    public interface IPlanAlimenticioServicio
    {
        Task<long> Add(PlanAlimenticioDto dto);
        Task Update(PlanAlimenticioDto dto);
        Task Delete(long id);
        Task<ICollection<PlanAlimenticioDto>> Get(bool eliminado, string cadenaBuscar);
        Task<PlanAlimenticioDto> GetById(long id);
        Task<int> GetNextCode();
        Task<IEnumerable<PlanAlimenticioDto>> GetByIdPaciente(long id);
        Task DuplicatePlan(long planId, long pacienteId);
        Task CalculateTotalCalories(long plandId);
        Task<PlanDiasDto> GetSortringComidas(long PlanId);
    }
}
