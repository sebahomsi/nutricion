using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NutricionWeb.Models.DatoAnalitico
{
    public class DatoAnaliticoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolHdl { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolLdl { get; set; }

        [Display(Name = "Colesterol Total")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolTotal { get; set; }

        [Display(Name = "Presion Diastolica")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PresionDiastolica { get; set; }

        [Display(Name = "Presion Sistolica")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PresionSistolica { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Trigliceridos { get; set; }

        public bool Eliminado { get; set; }
    }
}