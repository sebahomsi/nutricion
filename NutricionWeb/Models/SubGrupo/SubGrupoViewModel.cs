using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.SubGrupo
{
    public class SubGrupoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long GrupoId { get; set; }

        [Display(Name = "Grupo")]
        public string GrupoStr { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        public string EliminadoStr => Eliminado ? "SI" : "NO";
    }
}