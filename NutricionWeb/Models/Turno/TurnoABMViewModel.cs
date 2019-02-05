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

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Display(Name = "Código")]
        public int Numero { get; set; }

        [Display(Name = "Entrada")]
        [Required]
        public DateTime HorarioEntrada { get; set; }

        [Display(Name = "Salida")]
        [Required]
        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }

        public bool Eliminado { get; set; }
    }
}