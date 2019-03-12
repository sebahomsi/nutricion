using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.RecetaAlimento
{
    public class RecetaAlimentoViewModel
    {
        public long RecetaId { get; set; }

        [Display(Name = "Alimento")]
        public long AlimentoId { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }
    }
}