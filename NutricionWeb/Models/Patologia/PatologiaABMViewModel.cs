using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Patologia
{
    public class PatologiaABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}