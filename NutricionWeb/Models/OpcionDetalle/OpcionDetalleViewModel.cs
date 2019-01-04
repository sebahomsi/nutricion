using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.OpcionDetalle
{
    public class OpcionDetalleViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long OpcionId { get; set; }

        [Display(Name = "Opcion")]
        public string OpcionStr { get; set; }

        public long AlimentoId { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }

        public double Cantidad { get; set; }

        public long UnidadMedidaId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnidadMedidaStr { get; set; }

        [Display(Name = "Cantidad")]
        public string CantidadMostrar => Cantidad + UnidadMedidaStr;

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}