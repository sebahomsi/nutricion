using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IPlanAlimenticio
    {
        int Codigo { get; set; }

        [StringLength(130, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Comentarios { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long PacienteId { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        string Motivo { get; set; }

        DateTime Fecha { get; set; }

        bool Eliminado { get; set; }
    }
}
