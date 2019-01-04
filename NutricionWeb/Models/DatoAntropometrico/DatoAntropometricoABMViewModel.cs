using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.DatoAntropometrico
{
    public class DatoAntropometricoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Peso { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Altura { get; set; }

        [Display(Name = "Perimetro de Cadera")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PerimetroCadera { get; set; }

        [Display(Name = "Perimetro de Cintura")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string PerimetroCintura { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        [Display(Name = "Masa Grasa")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string MasaGrasa { get; set; }

        [Display(Name = "Masa Corporal")]
        [Required(ErrorMessage = "Campo Requerido")]
        public string MasaCorporal { get; set; }

        public bool Eliminado { get; set; }
    }
}