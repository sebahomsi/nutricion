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
        [Required(ErrorMessage = "Campo Obligatorio")]
        [Index("Index_Empleado_Legajo", IsUnique = true)]
        int Legajo { get; set; }
    }
}
