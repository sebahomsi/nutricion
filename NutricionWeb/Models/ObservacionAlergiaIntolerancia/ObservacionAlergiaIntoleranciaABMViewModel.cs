using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.ObservacionAlergiaIntolerancia
{
    public class ObservacionAlergiaIntoleranciaABMViewModel
    {
        public long ObservacionId { get; set; }
        public long AlergiaId { get; set; }

        [Display(Name = "Alergia/Intolerancia")]
        public string AlergiaStr { get; set; }
    }
}