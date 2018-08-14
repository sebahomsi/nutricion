using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Mail { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(10, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string FechaNac { get; set; }

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
