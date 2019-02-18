using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Mensaje
{
    public class MensajeABMViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Emisor")]
        public string EmailEmisor { get; set; }

        [Display(Name = "Receptor")]
        public string EmailReceptor { get; set; }

        public string Motivo { get; set; }

        [Display(Name = "Mensaje")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Cuerpo { get; set; }

        public bool Visto { get; set; }
    }

    public class MensajeViewModel
    {
        public long Id { get; set; }

        [Display(Name = "De")]
        public string EmailEmisor { get; set; }

        [Display(Name = "Para")]
        public string EmailReceptor { get; set; }

        public string Motivo { get; set; }

        [Display(Name = "Mensaje")]
        public string Cuerpo { get; set; }

        public bool Visto { get; set; }

        public string VistoStr => Visto ? "SI" : "NO";
    }
}