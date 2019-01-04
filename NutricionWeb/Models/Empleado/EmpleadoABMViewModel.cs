using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Persona;

namespace NutricionWeb.Models.Empleado
{
    public class EmpleadoABMViewModel : PersonaABMViewModel
    {
        public int Legajo { get; set; }
        
    }
}