﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using NutricionWeb.Models.Dia;

namespace NutricionWeb.Models.PlanAlimenticio
{
    public class PlanAlimenticioViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public string Motivo { get; set; }

        public DateTime Fecha { get; set; }

        public string Comentarios { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<DiaViewModel> Dias { get; set; }
    }
}