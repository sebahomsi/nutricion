using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string HorarioEntradaStr => HorarioEntrada.ToString("dd/MM/yyyy - HH:mm");

        public bool Eliminado { get; set; }

        public string EliminadoStr => Eliminado ? "SI" : "NO";

    }
}