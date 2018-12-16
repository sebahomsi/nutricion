using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.MicroNutriente
{
    public class MicroNutrienteViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}