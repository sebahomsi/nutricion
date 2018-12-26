﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.DatoAnalitico
{
    public class DatoAnaliticoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public string ColesterolHdl { get; set; }

        public string ColesterolLdl { get; set; }

        public string ColesterolTotal { get; set; }

        public string PresionDiastolica { get; set; }

        public string PresionSistolica { get; set; }

        public string Trigliceridos { get; set; }
    }
}