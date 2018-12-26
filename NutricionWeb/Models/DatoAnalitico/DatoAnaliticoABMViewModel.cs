using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.DatoAnalitico
{
    public class DatoAnaliticoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolHdl { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolLdl { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string ColesterolTotal { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string PresionDiastolica { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string PresionSistolica { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Trigliceridos { get; set; }
    }
}