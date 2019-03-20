using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IPago
    {

        int Codigo { get; set; }

        DateTime Fecha { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Concepto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        double Monto { get; set; }

        bool EstaEliminado { get; set; }

        long PacienteId { get; set; }
    }
}
