using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IOpcionDetalle
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long OpcionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long AlimentoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Unidad { get; set; }

        bool Eliminado { get; set; }
    }
}
