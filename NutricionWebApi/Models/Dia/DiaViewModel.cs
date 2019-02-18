using NutricionWebApi.Models.Comida;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWebApi.Models.Dia
{
    public class DiaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long PlanAlimenticioId { get; set; }

        [Display(Name = "Plan Alimenticio")]
        public string PlanAlimenticioStr { get; set; }

        public List<ComidaViewModel> Comidas { get; set; }
    }
}