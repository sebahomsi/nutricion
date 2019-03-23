using AutoMapper;
using Servicio.Interface.Turno;

namespace Servicio.Interface.AutoMapper
{
    public class TurnoProfile : Profile
    {
        public TurnoProfile()
        {
            var cfg = CreateMap<Dominio.Entidades.Turno, TurnoDto> ();

            cfg.ForMember(x => x.PacienteStr, e => e.MapFrom(x => x.Paciente.Apellido + " " + x.Paciente.Nombre));
        }
    }
}
