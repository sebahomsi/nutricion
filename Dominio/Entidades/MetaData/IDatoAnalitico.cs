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

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long PacienteId { get; set; }

        string ColesterolHdl { get; set; }

        string ColesterolLdl { get; set; }

        string ColesterolTotal { get; set; }

        string PresionDiastolica { get; set; }

        string PresionSistolica { get; set; }

        string Trigliceridos { get; set; }

        DateTime FechaMedicion { get; set; }

        string Glusemia { get; set; }
        string Insulina { get; set; }
        string VitaminaD { get; set; }
        string CPK { get; set; }
        string Creatinina { get; set; }
        string B12 { get; set; }
        string Zinc { get; set; }
        string Fosforo { get; set; }
        string GlobulosRojos { get; set; }
        string Hematocritos { get; set; }
        string Hemoglobina { get; set; }

        bool Eliminado { get; set; }
    }
}
