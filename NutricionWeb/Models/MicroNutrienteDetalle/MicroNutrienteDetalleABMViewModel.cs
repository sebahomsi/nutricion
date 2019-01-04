using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.MicroNutrienteDetalle
{
    public class MicroNutrienteDetalleABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long AlimentoId { get; set; }

        [Display(Name = "Alimento")]
        public string AlimentoStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long MicroNutrienteId { get; set; }

        [Display(Name = "MicroNutriente")]
        public string MicroNutrienteStr { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public double Cantidad { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        public long UnidadMedidaId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnidadMedidaStr { get; set; }
    }
}