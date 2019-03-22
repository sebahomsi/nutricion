using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.ComidaDetalle
{
    public class ComidaDetalleViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }

        public string OpcionStr { get; set; }
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }

    public class ComidaDetalleABMViewModel
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }

        public string OpcionStr { get; set; }
        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }
    }
}