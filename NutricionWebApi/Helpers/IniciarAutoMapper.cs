using AutoMapper;
using Servicio.Interface.AutoMapper;

namespace NutricionWebApi.Helpers
{
    public static class IniciarAutoMapper
    {
        public static void Iniciar()
        {
            Mapper.Initialize(cfg =>
            {
                
            });
        }
    }
}