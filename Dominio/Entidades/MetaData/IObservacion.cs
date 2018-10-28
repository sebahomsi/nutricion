using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IObservacion
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        long PacienteId { get; set; }

        bool Fumador { get; set; }

        bool BebeAlcohol { get; set; }

        string EstadoCivil { get; set; }

        bool TuvoHijo { get; set; }

        int CantidadHijo { get; set; }

        int CantidadSuenio { get; set; }

        bool Eliminado { get; set; }
    }
}
