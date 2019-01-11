using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.ObservacionPatologia
{
    public class ObservacionPatologiaABMViewModel
    {
        public long ObservacionId { get; set; }
        public long PatologiaId { get; set; }

        [Display(Name = "Patologia")]
        public string PatologiaStr { get; set; }
    }
}