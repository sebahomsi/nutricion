using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NutricionWeb.Models.Seguridad
{
    public class UsuarioRolViewModel
    {
        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Usuario")]
        public string UsuarioId { get; set; }

        [Display(Name = "Usuario")]
        public string Usuario { get; set; }

        [ScaffoldColumn(false)]
        [Required(ErrorMessage = "El campo {0} es Obligatorio.")]
        [Display(Name = "Rol")]
        public string RolId { get; set; }

        [Display(Name = "Rol")]
        public string Rol { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}