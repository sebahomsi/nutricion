using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface ITurno
    {
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long PacienteId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        int Numero { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        DateTime HorarioEntrada { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        DateTime HorarioSalida { get; set; }

        string Motivo { get; set; }
        

        bool Eliminado { get; set; }
    }
}
