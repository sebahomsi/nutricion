using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.OpcionDetalle
{
    public class OpcionDetalleABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long OpcionId { get; set; }

        public string OpcionStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public double Cantidad { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long UnidadMedidaId { get; set; }

        public string UnidadMedidaStr { get; set; }

        public bool Eliminado { get; set; }
    }
}