using System;
using System.ComponentModel.DataAnnotations;

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

        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDescripcion { get; set; }

        public string Motivo { get; set; }

        public string EstadoColor { get; set; }


        public bool Eliminado { get; set; }
    }
}