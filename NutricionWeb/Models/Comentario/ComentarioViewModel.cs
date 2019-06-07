using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Comentario
{
    public class ComentarioViewModel
    {
        public long Id { get; set; }

        public long PlanId { get; set; }
        public long ComidaId { get; set; }

        public long DiaId { get; set; }

        public string Texto { get; set; }
    }
}