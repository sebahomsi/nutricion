﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Turno
{
    public class TurnoViewModel
    {
        public long Id { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }

        public int Numero { get; set; }

        [Display(Name = "Entrada")]
        public DateTime HorarioEntrada { get; set; }

        [Display(Name = "Salida")]
        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }


        public bool Eliminado { get; set; }
    }
}