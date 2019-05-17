using NutricionWeb.Models.Persona;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteABMViewModel : PersonaABMViewModel
    {
        public int Codigo { get; set; }


        public bool TieneObservacion { get; set; }

    }
}