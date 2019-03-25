﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NutricionWeb.Models.ComidaDetalle;

namespace NutricionWeb.Models.Comida
{
    public class ComidaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long DiaId { get; set; }

        [Display(Name = "Dia")]
        public string DiaStr { get; set; }

        public List<ComidaDetalleViewModel> ComidasDetalles { get; set; }
    }
}