﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.MacroNutriente;

namespace NutricionWeb.Models.Alimento
{
    public class AlimentoABMViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        public long SubGrupoId { get; set; }

        [Display(Name = "SubGrupo")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        public string SubGrupoStr { get; set; }

        public bool Eliminado { get; set; }

        public MacroNutrienteViewModel MacroNutriente { get; set; }

    }
}