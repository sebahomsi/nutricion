using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Opcion;

namespace NutricionWeb.Models.ComidaDetalle
{
    public class ComidaDetalleViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }

        [Display(Name = "Receta")]
        public string OpcionStr { get; set; }

        [Display(Name = "Comida")]
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public OpcionViewModel Opcion { get; set; }
    }

    public class ComidaDetalleABMViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }

        [Display( Name = "Receta")]
        public string OpcionStr { get; set; }

        [Display(Name = "Comida")]
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }
    }
}