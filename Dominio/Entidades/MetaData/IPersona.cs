using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades.MetaData
{
    public interface IPersona
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Apellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(8, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        [Index(IsUnique = true)]
        string Dni { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [EmailAddress(ErrorMessage = "El campo debe tener formato de mail.")]
        string Mail { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        DateTime FechaNac { get; set; }

        int Sexo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(11, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(11, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Celular { get; set; }

        string Foto { get; set; }

        bool Eliminado { get; set; }
    }
}
