using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWeb.Models.Pago
{
    public class PagoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public double Monto { get; set; }

        public bool EstaEliminado { get; set; }

        public long PacienteId { get; set; }

        [Display(Name = "Paciente")]
        public string PacienteStr { get; set; }
    }
}