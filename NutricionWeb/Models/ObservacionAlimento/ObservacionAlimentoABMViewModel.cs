using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.ObservacionAlimento
{
    public class ObservacionAlimentoABMViewModel
    {
        public long ObservacionId { get; set; }
        public long AlimentoId { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }
    }
}