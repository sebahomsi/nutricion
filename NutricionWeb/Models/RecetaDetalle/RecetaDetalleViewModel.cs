using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.RecetaDetalle
{
    public class RecetaDetalleViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }

        [Display(Name = "Receta")]
        public long RecetaId { get; set; }

        [Display(Name = "Alimento")]
        public long AlimentoId { get; set; }

        [Display(Name = "Unidad")]
        public long UnidadMedidaId { get; set; }

        public decimal Cantidad { get; set; }
        public bool Eliminado { get; set; }

        public string EliminadoStr => Eliminado ? "SI" : "NO";

        [Display(Name = "Cantidad")]
        public string CantidadMostrar => Cantidad + UnidadMedidaStr;

        [Display(Name = "Receta")]
        public string RecetaStr { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }

        [Display(Name = "Unidad")]
        public string UnidadMedidaStr { get; set; }
    }

    public class RecetaDetalleABMViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }

        [Display(Name = "Receta")]
        public long RecetaId { get; set; }

        [Display(Name = "Alimento")]
        public long AlimentoId { get; set; }

        [Display(Name = "Unidad")]
        public long UnidadMedidaId { get; set; }

        public decimal Cantidad { get; set; }
        public bool Eliminado { get; set; }

        [Display(Name = "Receta")]
        public string RecetaStr { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }

        [Display(Name = "Unidad")]
        public string UnidadMedidaStr { get; set; }
    }
}