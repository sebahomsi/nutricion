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

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PacienteStr { get; set; }

        [Display(Name = "Código")]
        public int Numero { get; set; }

        [Display(Name = "Entrada")]
        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime HorarioEntrada { get; set; }

        [Display(Name = "Salida")]
        [Required(ErrorMessage = "Campo Requerido")]
        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }

        public bool Eliminado { get; set; }
    }
}