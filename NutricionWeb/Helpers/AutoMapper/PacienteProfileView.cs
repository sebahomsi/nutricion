using AutoMapper;
using NutricionWeb.Models.Paciente;
using Servicio.Interface.Paciente;

namespace NutricionWeb.Helpers.AutoMapper
{
    public class PacienteProfileView : Profile
    {
        public PacienteProfileView()
        {
            var cfg = CreateMap<PacienteDto, PacienteViewModel>();

            cfg.ForMember(x => x.FotoStr, o => o.Ignore());
        }
    }
}