using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IDatoAnalitico
    {
        int Codigo { get; set; }

        long PacienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string ColesterolHdl { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string ColesterolLdl { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string ColesterolTotal { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PresionDiastolica { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PresionSistolica { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Trigliceridos { get; set; }
    }
}
