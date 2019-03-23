using AutoMapper;
using Servicio.Interface.AutoMapper;

namespace NutricionWeb.Helpers.AutoMapper
{
    public static class IniciarAutoMapper
    {
        public static void Iniciar()
        {
            Mapper.Initialize(cfg =>
            {
                #region Dtos a View Models

                cfg.AddProfile(new TurnosProfileView());

                #endregion


                #region Entidades a Dtos

                cfg.AddProfile(new PlanAlimenticioProfile());
                cfg.AddProfile(new TurnoProfile());

                #endregion
            });
        }
    }
}