using AutoMapper;
using Servicio.Interface.DatoAnalitico;

namespace Servicio.Interface.AutoMapper
{
    public class DatoAnaliticoProfile : Profile
    {
        public DatoAnaliticoProfile()
        {
            var cfg = CreateMap<Dominio.Entidades.DatoAnalitico, DatoAnaliticoDto>();

            cfg.ForMember(x => x.PacienteStr, o => o.MapFrom(x => x.Paciente.Nombre + " " + x.Paciente.Apellido));
        }
    }
}
