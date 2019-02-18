using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.PlanAlimenticio
{
    public class PlanAlimenticioABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Motivo { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentarios { get; set; }

        public bool Eliminado { get; set; }
    }
}