using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Estrategia
{
    public class EstrategiaViewModel
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}