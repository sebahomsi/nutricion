using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.OpcionDetalle;

namespace NutricionWeb.Models.Opcion
{
    public class OpcionViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long ComidaId { get; set; }

        [Display(Name = "Comida")]
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<OpcionDetalleViewModel> OpcionDetalles { get; set; }
    }
}