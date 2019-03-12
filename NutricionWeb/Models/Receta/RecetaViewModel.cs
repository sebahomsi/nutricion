using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NutricionWeb.Models.Alimento;

namespace NutricionWeb.Models.Receta
{
    public class RecetaViewModel
    {
        public RecetaViewModel()
        {
            Alimentos = new List<AlimentoViewModel>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<AlimentoViewModel> Alimentos { get; set; }
    }

    public class RecetaABMViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
    }
}