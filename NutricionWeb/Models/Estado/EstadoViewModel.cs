using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NutricionWeb.Models.Turno;

namespace NutricionWeb.Models.Estado
{
    public class EstadoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        public string Color { get; set; }

        public bool Eliminado { get; set; }

        public List<TurnoViewModel> Turnos { get; set; }
    }
}