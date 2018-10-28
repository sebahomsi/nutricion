using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Observacion
{
    public class ObservacionViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public bool Fumador { get; set; }

        public bool BebeAlcohol { get; set; }

        public string EstadoCivil { get; set; }

        public bool? TuvoHijo { get; set; }

        public string CantidadHijo { get; set; }

        public string CantidadSuenio { get; set; }

        public bool Eliminado { get; set; }
    }
}