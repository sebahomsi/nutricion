using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.MacroNutriente
{
    public class MacroNutrienteViewModel
    {
        public long Id { get; set; }

        

        public decimal Proteina { get; set; }

        public decimal Grasa { get; set; }

        public decimal Energia { get; set; }

        [Display(Name = "Hidratos de Carbono")]
        public decimal HidratosCarbono { get; set; }

        public decimal Calorias { get; set; }

        public bool Eliminado { get; set; }

       
    }
}