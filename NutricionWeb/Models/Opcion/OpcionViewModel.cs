using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Opcion
{
    public class OpcionViewModel
    {
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long ComidaId { get; set; }

        public string ComidaStr { get; set; }

        public bool Eliminado { get; set; }
    }
}