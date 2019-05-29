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

        string ActividadFisica { get; set; }

        string AntecedentesFamiliares { get; set; }

        string Tabaco { get; set; }

        string Alcohol { get; set; }

        string Medicacion { get; set; }

        string HorasSuenio { get; set; }

        string RitmoEvacuatorio { get; set; }

        bool Eliminado { get; set; }
    }
}
