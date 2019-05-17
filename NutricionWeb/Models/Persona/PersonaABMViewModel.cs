using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;
using Org.BouncyCastle.Crypto.Digests;

namespace NutricionWeb.Models.Persona
{
    public class PersonaABMViewModel
    {
        public long Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Apellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(8, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        [Index(IsUnique = true)]
        public string Dni { get; set; }

        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0} debe tener formato de Email.")]
        public string Mail { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNac { get; set; }

        public int Sexo { get; set; }

        [StringLength(12, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Telefono { get; set; }

        [StringLength(12, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Celular { get; set; }

        [Display(Name = "Buscar Foto")]
        public HttpPostedFileBase Foto { get; set; }

        [Display(Name = "Foto")]
        public string FotoStr { get; set; }

        public bool Eliminado { get; set; }

        //[Range(1, long.MaxValue,ErrorMessage = "Campo Obligatorio")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [Display(Name = "Establecimiento")]
        public long EstablecimientoId { get; set; }

        public IEnumerable<SelectListItem> Sexos { get; set; }

        public IEnumerable<SelectListItem> Establecimientos { get; set; }
    }
}