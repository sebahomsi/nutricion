using AutoMapper;
using NutricionWeb.Models.DatoAntropometrico;
using Servicio.Interface.DatoAntropometrico;

namespace NutricionWeb.Helpers.AutoMapper
{
    public class DatoAntropometricoProfileView : Profile
    {
        public DatoAntropometricoProfileView()
        {
            var cfg = CreateMap<DatoAntropometricoDto, DatoAntropometricoViewModel>();

            cfg.ForMember(x => x.FotoPerfilStr, o => o.Ignore());
            cfg.ForMember(x => x.FotoFrenteStr, o => o.Ignore());

            cfg.ForMember(x => x.PacienteStr, o => o.MapFrom(x => x.PacienteStr));
        }
    }
}