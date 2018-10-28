using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Persona;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public bool TieneAnalitico { get; set; }
    }
}