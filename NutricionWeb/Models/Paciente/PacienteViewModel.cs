using System;
using NutricionWeb.Models.Persona;
using NutricionWeb.Models.Turno;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }

        [Display(Name = "Fecha de Alta")]
        public DateTime FechaAlta { get; set; }

        public string FechaAltaStr => FechaAlta.Date.ToString("dd/MM/yyyy");

        public bool TieneObservacion { get; set; }

        public List<TurnoViewModel> Turnos { get; set; }
    }
}