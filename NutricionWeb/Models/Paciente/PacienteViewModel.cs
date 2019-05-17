using NutricionWeb.Models.Persona;
using NutricionWeb.Models.Turno;
using System.Collections.Generic;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }

        public bool TieneObservacion { get; set; }

        public List<TurnoViewModel> Turnos { get; set; }
    }
}