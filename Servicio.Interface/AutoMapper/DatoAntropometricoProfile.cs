using AutoMapper;
using Servicio.Interface.DatoAntropometrico;

namespace Servicio.Interface.AutoMapper
{
    public class DatoAntropometricoProfile : Profile
    {
        public DatoAntropometricoProfile()
        {
            var cfg = CreateMap<Dominio.Entidades.DatoAntropometrico, DatoAntropometricoDto>();

            cfg.ForMember(x => x.PacienteStr, o =>o.MapFrom(x=>x.Paciente.Nombre+" "+x.Paciente.Apellido));
            
        }
    }
}
