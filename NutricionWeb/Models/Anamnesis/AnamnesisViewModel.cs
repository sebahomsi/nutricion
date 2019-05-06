using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Anamnesis
{
    public class AnamnesisViewModel
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}