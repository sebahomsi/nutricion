using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.SubGrupo
{
    public class SubGrupoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long GrupoId { get; set; }

        public string GrupoStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }
    }
}