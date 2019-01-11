using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.Patologia;

namespace NutricionWeb.Models.Observacion
{
    public class ObservacionViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public bool Fumador { get; set; }

        [Display(Name = "Fumador")]
        public string FumadorStr => Fumador ? "SI" : "NO";

        [Display(Name = "Bebe Alcohol")]
        public bool BebeAlcohol { get; set; }

        [Display(Name = "Bebe Alcohol")]
        public string BebeAlcoholStr => BebeAlcohol ? "SI" : "NO";

        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Tuvo Hijos")]
        public bool TuvoHijo { get; set; }

        [Display(Name = "Tuvo Hijos")]
        public string TuvoHijoStr => TuvoHijo ? "SI" : "NO";

        [Display(Name = "Cantidad de Hijos")]
        public string CantidadHijo { get; set; }

        [Display(Name = "Horas que Duerme")]
        public string CantidadSuenio { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<PatologiaViewModel> Patologias { get; set; }

        public List<AlergiaIntoleranciaViewModel> AlergiasIntolerancias { get; set; }

        public List<AlimentoViewModel> Alimentos { get; set; }


    }
}