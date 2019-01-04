using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Persona
{
    public class PersonaViewModel
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [Display(Name = "Apellido y Nombre")]
        [ScaffoldColumn(false)]
        public string ApyNom => $"{Apellido} {Nombre}";

        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }

        [Display(Name = "Nacimiento")]
        public DateTime FechaNac { get; set; }

        [Display(Name = "Nacimiento")]
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

        [Display(Name = "Sexo")]
        [ScaffoldColumn(false)]
        public string SexoStr => Sexo == 1 ? "Masculino" : "Femenino";

        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string FotoStr { get; set; }
        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}