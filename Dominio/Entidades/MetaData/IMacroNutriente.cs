using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IMacroNutriente
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        long AlimentoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Proteina { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Grasa { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Energia { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string HidratosCarbono { get; set; }

        bool Eliminado { get; set; }
    }
}
