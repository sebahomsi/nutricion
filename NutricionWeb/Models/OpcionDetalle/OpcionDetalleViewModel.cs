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

        public string OpcionStr { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public double Cantidad { get; set; }

        public long UnidadMedidaId { get; set; }

        public string UnidadMedidaStr { get; set; }

        public string CantidadMostrar => Cantidad + UnidadMedidaStr;

        public bool Eliminado { get; set; }
    }
}