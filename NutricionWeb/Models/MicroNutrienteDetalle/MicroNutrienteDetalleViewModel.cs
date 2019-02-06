using System;
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

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }

        public long MicroNutrienteId { get; set; }

        [Display(Name = "MicroNutriente")]
        public string MicroNutrienteStr { get; set; }

        public double Cantidad { get; set; }

        [Display(Name = "Cantidad")]
        public string CantidadMostrar => Cantidad + UnidadMedidaStr;

        public long UnidadMedidaId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnidadMedidaStr { get; set; }
    }
}