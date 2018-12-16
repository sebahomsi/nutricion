﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.MicroNutrienteDetalle
{
    public class MicroNutrienteDetalleViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public long MicroNutrienteId { get; set; }

        public string MicroNutrienteStr { get; set; }

        public double Cantidad { get; set; }

        public long UnidadMedidaId { get; set; }

        public string UnidadMedidaStr { get; set; }
    }
}