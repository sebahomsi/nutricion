using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Alimento
{
    public class AlimentoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "SubGrupo")]
        public long SubGrupoId { get; set; }

        [Display(Name = "SubGrupo")]
        public string SubGrupoStr { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public long? MacroNutrienteId { get; set; }

        public bool TieneMacroNutriente { get; set; }

    }
}