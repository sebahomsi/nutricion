﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Persona;
using Servicio.Interface.PlanAlimenticio;

namespace Servicio.Interface.Paciente
{
    public class PacienteDto : PersonaDto
    {
        public PacienteDto()
        {
            DatosAntropometricos = new List<DatoAntropometricoDto>();
            AlimentosRechazados = new List<AlimentoDto>();
            PlanesAlimenticios = new List<PlanAlimenticioDto>();
        }
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public bool TieneAnalitico { get; set; }
        
        public List<DatoAntropometricoDto> DatosAntropometricos { get; set; }
        public List<AlimentoDto> AlimentosRechazados { get; set; }
        public List<PlanAlimenticioDto> PlanesAlimenticios { get; set; }
    }
}