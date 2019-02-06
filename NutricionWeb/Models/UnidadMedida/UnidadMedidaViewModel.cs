using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.UnidadMedida
{
    public class UnidadMedidaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Abreviatura { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}