using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IDatoAntropometrico
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long PacienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PesoActual { get; set; }
        

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PesoHabitual { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PesoDeseado { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PesoIdeal { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PerimetroCuello { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Altura { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PerimetroCadera { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PerimetroCintura { get; set; }

        DateTime FechaMedicion { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string MasaGrasa { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string MasaCorporal { get; set; }

        string Foto { get; set; }

        bool Eliminado { get; set; }
    }
}
