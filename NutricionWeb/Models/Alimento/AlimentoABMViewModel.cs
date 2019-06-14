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


        [Required(ErrorMessage = "Campo Obligatorio")]
        [StringLength(250, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
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