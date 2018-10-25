using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IMicroNutrienteDetalle
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long AlimentoId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long MicroNutrienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        double Cantidad { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long UnidadMedidaId { get; set; }
    }
}
