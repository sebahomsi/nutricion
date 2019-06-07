using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NutricionWeb.Models.Opcion
{
    public class OpcionABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public long? SubGrupoId { get; set; }
        public IEnumerable<SelectListItem> SubGrupos { get; set; }
    }
}