using System;

namespace NutricionWebApi.Models.Persona
{
    public class PersonaWebApiViewModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ApyNom => $"{Apellido} {Nombre}";
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public DateTime FechaNac { get; set; }
        public string FechaNacStr => FechaNac.Date.ToString("yyyy-MM-dd");

        public int Edad
        {
            get
            {
                var edad = DateTime.Now.Year - FechaNac.Date.Year;

                DateTime nacimientoAhora = FechaNac.Date.AddYears(edad);

                if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
                {
                    edad--;
                }

                return edad;
            }
        }
        public int Sexo { get; set; }
        public string SexoStr => Sexo == 1 ? "Masculino" : "Femenino";
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string FotoStr { get; set; }
        public bool Eliminado { get; set; }
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}