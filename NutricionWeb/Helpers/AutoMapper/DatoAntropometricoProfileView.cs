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

            cfg.ForMember(x => x.FotoPerfilStr, o => o.MapFrom(x => x.Foto.Split('|')[1]));
            cfg.ForMember(x => x.FotoFrenteStr, o => o.MapFrom(x => x.Foto.Split('|')[0]));

            cfg.ForMember(x => x.PacienteStr, o => o.MapFrom(x => x.PacienteStr));
        }
    }
}