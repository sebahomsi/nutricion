using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Turno
{
    public class TurnoABMViewModel
    {
        public long Id { get; set; }

        [Required]
        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public int Numero { get; set; }

        [Required]
        public DateTime HorarioEntrada { get; set; }

        [Required]
        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }


        public bool Eliminado { get; set; }
    }
}