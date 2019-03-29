using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.MacroNutriente
{
    public class MacroNutrienteABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long AlimentoId { get; set; }

        [Display(Name = "Alimento")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string AlimentoStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public int Proteina { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public int Grasa { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public int Energia { get; set; }

        [Display(Name = "Hidratos de Carbono")]
        [Required(ErrorMessage = "Campo Requerido")]
        public int HidratosCarbono { get; set; }

        public int Calorias { get; set; }

        public bool Eliminado { get; set; }
    }
}