using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Establecimiento
{
    public class EstablecimientoViewModel
    {
        public long Id { get; set; }

        public string Nombre { get; set; }

        [Display(Name = "Profesional a Cargo")]
        public string Profesional { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Instagram { get; set; }

        [Display(Name = "Teléfono")]
        [Phone]
        public string Telefono { get; set; }

        [Display(Name = "Horario de Atención")]
        public string Horario { get; set; }
    }
}