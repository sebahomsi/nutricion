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
                cfg.AddProfile(new DatoAntropometricoProfileView());
                cfg.AddProfile(new PacienteProfileView());
                
                #endregion


                #region Entidades a Dtos

                cfg.AddProfile(new PlanAlimenticioProfile());
                cfg.AddProfile(new TurnoProfile());
                cfg.AddProfile(new DatoAntropometricoProfile());
                cfg.AddProfile(new DatoAnaliticoProfile());
                #endregion
            });
        }
    }
}