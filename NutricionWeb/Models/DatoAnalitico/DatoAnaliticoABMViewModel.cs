﻿using System;
using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.DatoAnalitico
{
    public class DatoAnaliticoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public string ColesterolHdl { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        public string ColesterolLdl { get; set; }

        [Display(Name = "Colesterol Total")]
        public string ColesterolTotal { get; set; }

        [Display(Name = "Presion Diastolica")]
        public string PresionDiastolica { get; set; }

        [Display(Name = "Presion Sistolica")]
        public string PresionSistolica { get; set; }

        public string Trigliceridos { get; set; }
        [Display(Name = "Glucemia")]
        public string Glusemia { get; set; }
        public string Insulina { get; set; }
        [Display(Name = "Vitamina D")]
        public string VitaminaD { get; set; }
        public string CPK { get; set; }
        public string Creatinina { get; set; }
        public string B12 { get; set; }
        public string Zinc { get; set; }
        public string Fosforo { get; set; }
        [Display(Name = "Globulos Rojos")]
        public string GlobulosRojos { get; set; }
        [Display(Name = "Hematocrito")]
        public string Hematocritos { get; set; }
        public string Hemoglobina { get; set; }

        public bool Eliminado { get; set; }
    }
}