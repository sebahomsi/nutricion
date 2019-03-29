﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MacroNutriente;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.Observacion;
using Servicio.Interface.Opcion;

namespace Servicio.Interface.Alimento
{
    public class AlimentoDto
    {
        public AlimentoDto()
        {
            MicroNutrienteDetalles = new List<MicroNutrienteDetalleDto>();
            Opciones = new List<OpcionDto>();
            Observaciones = new List<ObservacionDto>();
        }
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long SubGrupoId { get; set; }

        public string SubGrupoStr { get; set; }

        public bool Eliminado { get; set; }


        public MacroNutrienteDto MacroNutriente { get; set; }

        public List<MicroNutrienteDetalleDto> MicroNutrienteDetalles { get; set; }
        public List<OpcionDto> Opciones { get; set; }
        public List<ObservacionDto> Observaciones { get; set; }

        
    }
}
