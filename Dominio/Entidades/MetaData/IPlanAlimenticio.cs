using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
