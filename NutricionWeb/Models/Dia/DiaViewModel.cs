using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Comida;

namespace NutricionWeb.Models.Dia
{
    public class DiaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long PlanAlimenticioId { get; set; }

        public string PlanAlimenticioStr { get; set; }

        public List<ComidaViewModel> Comidas { get; set; }
    }
}