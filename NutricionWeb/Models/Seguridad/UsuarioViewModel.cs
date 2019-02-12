using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Seguridad
{
    public class UsuarioViewModel
    {
        public UsuarioViewModel()
        {
            Roles = new List<RolViewModel>();
        }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string UsuarioId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        public string Nombre { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        public List<RolViewModel> Roles { get; set; }
    }
}