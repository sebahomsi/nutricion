using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Estrategia;
using NutricionWeb.Models.Objetivo;
using NutricionWeb.Models.Paciente;

namespace NutricionWeb.Models.Mail
{
    public class CuerpoMailViewModel
    {

        public PacienteViewModel Paciente { get; set; }

        public DatoAntropometricoViewModel DatoAntropometrico { get; set; }

        public ObjetivoViewModel Objetivo { get; set; }

        public EstrategiaViewModel Estrategia { get; set; }


    }
}