using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.MacroNutriente;

namespace NutricionWeb.Models.Alimento
{
    public class AlimentoABMViewModel
    {
        public AlimentoABMViewModel()
        {
            MacroNutriente = new MacroNutrienteViewModel();
        }
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "SubGrupo")]
        public long SubGrupoId { get; set; }

        [Display(Name = "SubGrupo")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string SubGrupoStr { get; set; }

        public bool Eliminado { get; set; }

        public MacroNutrienteViewModel MacroNutriente { get; set; }

        public IEnumerable<SelectListItem> SubGrupos { get; set; }

    }
}