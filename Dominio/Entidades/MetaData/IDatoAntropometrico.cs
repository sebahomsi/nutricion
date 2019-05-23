using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades.MetaData
{
    public interface IDatoAntropometrico
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long PacienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string PesoActual { get; set; }

        string PesoHabitual { get; set; }

        string PesoDeseado { get; set; }

        string PesoIdeal { get; set; }

        string PerimetroCuello { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Altura { get; set; }

        string PerimetroCadera { get; set; }

        string PerimetroCintura { get; set; }

        DateTime FechaMedicion { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string MasaGrasa { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string MasaCorporal { get; set; }

        string Foto { get; set; }

        decimal PliegueSubescapular { get; set; }

        decimal PliegueSuprailiaco { get; set; }

        decimal PliegueAbdominal { get; set; }

        decimal PliegueMuslo { get; set; }

        decimal PlieguePierna { get; set; }

        decimal TotalPliegues { get; set; }

        bool Eliminado { get; set; }
    }
}
