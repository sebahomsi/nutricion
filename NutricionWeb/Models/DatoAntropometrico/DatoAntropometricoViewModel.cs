using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.DatoAntropometrico
{
    public class DatoAntropometricoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Display(Name = "Peso Actual")]
        public string PesoActual { get; set; }

        [Display(Name = "Peso Habitual")]
        public string PesoHabitual { get; set; }

        [Display(Name = "Peso Deseado")]
        public string PesoDeseado { get; set; }

        [Display(Name = "Peso Ideal")]
        public string PesoIdeal { get; set; }

        [Display(Name = "Perimetro de Cuello")]
        public  string PerimetroCuello { get; set; }

        public string Altura { get; set; }

        [Display(Name = "Perimetro de Cadera")]
        public string PerimetroCadera { get; set; }

        [Display(Name = "Perimetro de Cintura")]
        public string PerimetroCintura { get; set; }

        [Display(Name = "Fecha de Medicion")]
        public DateTime FechaMedicion { get; set; }

        [Display(Name = "Fecha y Hora")]
        public string FechaMedicionStr => FechaMedicion.ToString("dd/MM/yyyy");

        [Display(Name = "Fecha y Hora")]
        public string HoraMedicionStr => FechaMedicion.ToString("HH:mm");

        [Display(Name = "Masa Grasa")]
        public string MasaGrasa { get; set; }

        [Display(Name = "Masa Corporal")]
        public string MasaCorporal { get; set; }

        public bool Eliminado { get; set; }

        public string FotoStr { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}