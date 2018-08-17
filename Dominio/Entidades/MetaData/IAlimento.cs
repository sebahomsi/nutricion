using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IAlimento
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long SubGrupoId { get; set; }

        long? MacroNutrienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Descripcion { get; set; }

        bool Eliminado { get; set; }

        bool TieneMacroNutriente { get; set; }
    }
}
