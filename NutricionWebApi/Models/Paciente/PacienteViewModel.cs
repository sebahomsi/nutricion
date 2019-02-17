using NutricionWebApi.Models.Persona;

namespace NutricionWebApi.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public string EstadoStr => Estado ? "ACTIVO" : "INACTIVO";

        public bool TieneObservacion { get; set; }
    }
}