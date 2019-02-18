using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NutricionMovil
{
    public class Dia
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
