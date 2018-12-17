﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.DatoAntropometrico
{
    public class DatoAntropometricoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public string Peso { get; set; }

        public string Altura { get; set; }

        public string PerimetroCadera { get; set; }

        public string PerimetroCintura { get; set; }

        public DateTime FechaMedicion { get; set; }

        public string MasaGrasa { get; set; }

        public string MasaCorporal { get; set; }

        public bool Eliminado { get; set; }
    }
}