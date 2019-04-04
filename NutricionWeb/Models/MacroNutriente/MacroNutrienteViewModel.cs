using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.MacroNutriente
{
    public class MacroNutrienteViewModel
    {
        public long Id { get; set; }

        

        public int Proteina { get; set; }

        public int Grasa { get; set; }

        public int Energia { get; set; }

        [Display(Name = "Hidratos de Carbono")]
        public int HidratosCarbono { get; set; }

        public int Calorias { get; set; }

        public bool Eliminado { get; set; }

       
    }
}