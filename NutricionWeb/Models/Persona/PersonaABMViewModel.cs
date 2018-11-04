using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(150, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Direccion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [EmailAddress(ErrorMessage = "El campo {0} debe tener formato de Email.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public DateTime FechaNac { get; set; }

        public int Sexo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(12, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Telefono { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(12, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        public string Celular { get; set; }
        public string Foto { get; set; }
        public bool Eliminado { get; set; }

        public IEnumerable<SelectListItem> Sexos { get; set; }
    }
}