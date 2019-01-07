using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Persona;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Turno;

namespace Servicio.Interface.Paciente
{
    public class PacienteDto : PersonaDto
    {
        public PacienteDto()
        {
            DatosAntropometricos = new List<DatoAntropometricoDto>();
            PlanesAlimenticios = new List<PlanAlimenticioDto>();
            DatosAnaliticos = new List<DatoAnaliticoDto>();
            Turnos = new List<TurnoDto>();
        }
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public bool TieneObservacion { get; set; }

        public List<DatoAntropometricoDto> DatosAntropometricos { get; set; }
        public List<PlanAlimenticioDto> PlanesAlimenticios { get; set; }
        public List<DatoAnaliticoDto> DatosAnaliticos { get; set; }
        public List<TurnoDto> Turnos { get; set; }

    }
}
