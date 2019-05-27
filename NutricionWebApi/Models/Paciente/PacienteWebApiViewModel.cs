using NutricionWebApi.Models.Persona;

namespace NutricionWebApi.Models.Paciente
{
    public class PacienteWebApiViewModel : PersonaWebApiViewModel
    {
        public int Codigo { get; set; }

        public bool TieneObservacion { get; set; }
    }
}