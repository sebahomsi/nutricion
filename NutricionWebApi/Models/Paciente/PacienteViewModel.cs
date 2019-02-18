using NutricionWebApi.Models.DatoAnalitico;
using NutricionWebApi.Models.DatoAntropometrico;
using NutricionWebApi.Models.Persona;
using NutricionWebApi.Models.PlanAlimenticio;
using NutricionWebApi.Models.Turno;
using System.Collections.Generic;

namespace NutricionWebApi.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }



        public bool TieneObservacion { get; set; }

        public List<PlanAlimenticioViewModel> PlanesAlimenticios { get; set; }
        public List<DatoAntropometricoViewModel> DatosAntropometricos { get; set; }
        public List<DatoAnaliticoViewModel> DatosAnaliticos { get; set; }
        public List<TurnoViewModel> Turnos { get; set; }
    }
}