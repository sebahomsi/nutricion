using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.OpcionDetalle;
using Servicio.Interface.Alimento;

namespace NutricionWeb.Models.Opcion
{
    public class OpcionViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public long? SubGrupoRecetaId { get; set; }

        public List<OpcionDetalleViewModel> OpcionDetalles { get; set; }
        public List<ComidaDetalleViewModel> ComidasDetalles { get; set; }

    }

    public class BuscarRecetaViewModel
    {
        [NotMapped]
        public ICollection<AlimentoDto> Alimentos { get; set; }

        public string[] AlimentosSeleccionados { get; set; }
    }
}