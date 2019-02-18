using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NutricionMovil
{
    public class PlanAlimenticio
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public string Motivo { get; set; }

        public DateTime Fecha { get; set; }

        [Display(Name = "Fecha y Hora")]
        public string FechaStr => Fecha.ToString("dd/MM/yyyy");

        public string HoraStr => Fecha.ToString("HH:mm");


        public string Comentarios { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<Dia> Dias { get; set; }
    }
}
