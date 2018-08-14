using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IMicroNutriente
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Descripcion { get; set; }

        bool Eliminado { get; set; }
    }
}
