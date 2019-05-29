using System;
using System.ComponentModel.DataAnnotations;
using NutricionWeb.Models.Persona;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteABMViewModel : PersonaABMViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        public bool TieneObservacion { get; set; }

    }
}