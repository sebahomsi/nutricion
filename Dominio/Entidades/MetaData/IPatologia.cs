using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IPatologia
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Descripcion { get; set; }

        bool Eliminado { get; set; }
    }
}
