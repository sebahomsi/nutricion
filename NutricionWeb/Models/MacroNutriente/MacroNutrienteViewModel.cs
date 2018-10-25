using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.MacroNutriente
{
    public class MacroNutrienteViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Proteina { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Grasa { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Energia { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string HidratosCarbono { get; set; }

        public bool Eliminado { get; set; }
    }
}