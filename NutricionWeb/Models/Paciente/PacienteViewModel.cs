using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using NutricionWeb.Models.DatoAnalitico;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Persona;
using NutricionWeb.Models.PlanAlimenticio;
using NutricionWeb.Models.Turno;

namespace NutricionWeb.Models.Paciente
{
    public class PacienteViewModel : PersonaViewModel
    {
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public string EstadoStr => Estado ? "ACTIVO" : "INACTIVO";

        public bool TieneObservacion { get; set; }

        public List<PlanAlimenticioViewModel> PlanesAlimenticios { get; set; }
        public List<DatoAntropometricoViewModel> DatosAntropometricos { get; set; }
        public List<DatoAnaliticoViewModel> DatosAnaliticos { get; set; }
        public List<TurnoViewModel> Turnos { get; set; }

    }
}