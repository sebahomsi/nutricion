using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Grupo
{
    public class GrupoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}