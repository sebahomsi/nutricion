using System.Collections.Generic;

namespace NutricionWeb.Helpers
{
    public static class Diccionario
    {
        private static readonly IDictionary<int, string> Comidas;
        private static readonly IDictionary<int, string> Colores;

        static Diccionario()
        {
            Comidas = new Dictionary<int, string>
            {
                {1, "Desayuno"},
                {2, "Media Mañana"},
                {3, "Almuerzo"},
                {4, "Merienda"},
                {5, "Media Tarde"},
                {6, "Cena"}
            };

            Colores = new Dictionary<int, string>
            {
                {1, "#ffc0d7"},
                {2, "#c1ffc0"},
                {3, "#c0c5ff"},
                {4, "#ffc9c0"},
                {5, "#dbffc0"},
                {6, "#fbffc0"}
            };


        }

        public static string ObtenerComidaStr(int numero)
        {
            return Comidas[numero];
        }

        public static string ObtenerComidaColor(int numero)
        {
            return Colores[numero];
        }

    }
}