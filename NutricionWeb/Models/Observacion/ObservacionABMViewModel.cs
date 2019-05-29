using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Observacion
{
    public class ObservacionABMViewModel
    {

        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Display(Name = "Actividad Fisica ")]
        public string ActividadFisica { get; set; }

        [Display(Name = "Antecedentes Familiares")]
        public string AntecedentesFamiliares { get; set; }

        public string Tabaco { get; set; }

        public string Alcohol { get; set; }

        public string Medicacion { get; set; }

        [Display(Name = "Horas de Sueño")]
        public string HorasSuenio { get; set; }

        [Display(Name = "Ritmo Evacuatorio")]
        public string RitmoEvacuatorio { get; set; }

        public bool Eliminado { get; set; }
    }
}