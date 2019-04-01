using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IMacroNutriente
    {
        [Required(ErrorMessage = "Campo Obligatorio")]
        int Proteina { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        int Grasa { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        int Energia { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        int HidratosCarbono { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        int Calorias { get; set; }

        bool Eliminado { get; set; }
    }
}
