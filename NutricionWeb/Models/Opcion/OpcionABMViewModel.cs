using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.SubGrupoReceta;

namespace NutricionWeb.Models.Opcion
{
    public class OpcionABMViewModel
    {
        public OpcionABMViewModel()
        {
            SubGruposId = new List<long?>();
            SubGruposVm = new List<SubGrupoRecetaViewModel>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public IEnumerable<long?> SubGruposId { get; set; }

        public IEnumerable<SubGrupoRecetaViewModel> SubGruposVm { get; set; }

        public IEnumerable<SelectListItem> SubGrupos { get; set; }
    }
}