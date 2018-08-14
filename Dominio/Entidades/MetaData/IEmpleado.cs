using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IEmpleado
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(6, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        [Index("Index_Empleado_Legajo", IsUnique = true)]
        string Legajo { get; set; }
    }
}
