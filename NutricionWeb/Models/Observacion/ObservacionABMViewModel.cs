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
        [Required(ErrorMessage = "Campo Requerido")]
        public string PacienteStr { get; set; }

        public bool Fumador { get; set; }

        [Display(Name = "Bebe Alcohol")]
        public bool BebeAlcohol { get; set; }

        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Tuvo Hijos")]
        public bool TuvoHijo { get; set; }

        [Display(Name = "Cantidad de Hijos")]
        public string CantidadHijo { get; set; }

        [Display(Name = "Horas que Duerme")]
        public string CantidadSuenio { get; set; }

        public bool Eliminado { get; set; }
    }
}