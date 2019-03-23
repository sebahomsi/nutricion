using AutoMapper;
using Servicio.Interface.PlanAlimenticio;

namespace Servicio.Interface.AutoMapper
{
    public class PlanAlimenticioProfile : Profile
    {
        public PlanAlimenticioProfile()
        {
            var cfg = CreateMap<Dominio.Entidades.PlanAlimenticio, PlanAlimenticioDto>();

            cfg.ForMember(x => x.PacienteStr, p => p.MapFrom(e => e.Paciente.Apellido + " " + e.Paciente.Nombre));

        }
    }
}
