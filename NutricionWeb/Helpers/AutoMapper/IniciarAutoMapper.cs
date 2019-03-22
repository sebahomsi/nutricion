using AutoMapper;

namespace NutricionWeb.Helpers.AutoMapper
{
    public static class IniciarAutoMapper
    {
        public static void Iniciar()
        {

            #region Dtos a View Models

            Mapper.Initialize(cfg => cfg.AddProfile<TurnosProfileView>());

            #endregion

            #region Entidades a Dtos

           

            #endregion

        }
    }
}