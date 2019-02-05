using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NutricionWeb.Models.DatoAnalitico
{
    public class DatoAnaliticoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public string ColesterolHdl { get; set; }

        public string ColesterolLdl { get; set; }

        [Display(Name = "Colesterol Total")]
        public string ColesterolTotal { get; set; }

        [Display(Name = "Presion Diastolica")]
        public string PresionDiastolica { get; set; }

        [Display(Name = "Presion Sistolica")]
        public string PresionSistolica { get; set; }

        public string Trigliceridos { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        [Display(Name = "Fecha y Hora")]
        public string FechaMedicionStr => FechaMedicion.ToString("dd/MM/yyyy");

        public string HoraMedicionStr => FechaMedicion.ToString("HH:mm");

        public bool Eliminado { get; set; }
    }
}