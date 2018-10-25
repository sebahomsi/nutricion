using NutricionWeb.Models.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Empleado
{
    public class EmpleadoViewModel: PersonaViewModel
    {
        public int Legajo { get; set; }
    }
}