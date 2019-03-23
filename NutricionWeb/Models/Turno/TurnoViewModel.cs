using System;
using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.Turno
{
    public class TurnoViewModel
    {
        public long Id { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Display(Name = "Código")]
        public int Numero { get; set; }

        [Display(Name = "Entrada")]
        public DateTime HorarioEntrada { get; set; }

        [Display(Name = "Salida")]
        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }

        [Display(Name = "Fecha y Hora")]
        public string HorarioEntradaStr => HorarioEntrada.ToString("HH:mm");

        public string FechaEntradaStr => HorarioEntrada.ToString("dd/MM/yyyy");

        [Display(Name = "Fecha y Hora")]
        public string HorarioSalidaStr => HorarioSalida.ToString("HH:mm");

        public string FechaSalidaStr => HorarioSalida.ToString("dd/MM/yyyy");

        public long EstadoId { get; set; }

        [Display(Name = "Estado")]
        public string EstadoDescripcion { get; set; }

        public bool Eliminado { get; set; }

        public string EliminadoStr => Eliminado ? "SI" : "NO";

    }
}