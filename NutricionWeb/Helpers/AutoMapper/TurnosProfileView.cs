using AutoMapper;
using NutricionWeb.Models.Turno;
using Servicio.Interface.Turno;

namespace NutricionWeb.Helpers.AutoMapper
{
    public class TurnosProfileView : Profile
    {
        public TurnosProfileView()
        {
            var cfg = CreateMap<TurnoDto, TurnoViewModel> ();
        }
    }
}