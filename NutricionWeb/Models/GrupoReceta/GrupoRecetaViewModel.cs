using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.GrupoReceta
{
    public class GrupoRecetaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}