using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Persona;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteABMViewModel : PersonaABMViewModel
    {
        public int Codigo { get; set; }


        public bool TieneObservacion { get; set; }

    }
}