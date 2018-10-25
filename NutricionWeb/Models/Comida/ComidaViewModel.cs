﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Comida
{
    public class ComidaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long DiaId { get; set; }

        public string DiaStr { get; set; }
    }
}