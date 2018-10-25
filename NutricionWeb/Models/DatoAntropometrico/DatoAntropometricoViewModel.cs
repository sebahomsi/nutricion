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

        [Required(ErrorMessage = "Campo Requerido")]
        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Peso { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Altura { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string PerimetroCadera { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string PerimetroCintura { get; set; }

        public DateTime FechaMedicion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string MasaGrasa { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string MasaCorporal { get; set; }

        public bool Eliminado { get; set; }
    }
}