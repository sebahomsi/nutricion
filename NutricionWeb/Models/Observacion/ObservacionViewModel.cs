﻿using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.Patologia;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NutricionWeb.Models.Observacion
{
    public class ObservacionViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        [Display(Name = "Actividad Fisica ")]
        public string ActividadFisica { get; set; }

        [Display(Name = "Antecedentes Familiares")]
        public string AntecedentesFamiliares { get; set; }

        public string Tabaco { get; set; }

        public string Alcohol { get; set; }

        public string Medicacion { get; set; }

        [Display(Name = "Horas de Sueño")]
        public string HorasSuenio { get; set; }

        [Display(Name = "Ritmo Evacuatorio")]
        public string RitmoEvacuatorio { get; set; }

        public bool Eliminado { get; set; }

        [Display(Name = "Eliminado")]
        [ScaffoldColumn(false)]
        public string EliminadoStr => Eliminado ? "SI" : "NO";

        public List<PatologiaViewModel> Patologias { get; set; }

        public List<AlergiaIntoleranciaViewModel> AlergiasIntolerancias { get; set; }

        public List<AlimentoViewModel> Alimentos { get; set; }
    }
}