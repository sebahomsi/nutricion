using NutricionWeb.Models.Comida;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace NutricionWeb.Models.PlanAlimenticio
{    public class PlanAlimenticioVistaViewModel
    {
        public List<ComidaViewModel> Desayunos { get; set; }
        public List<ComidaViewModel> MediaMañana { get; set; }
        public List<ComidaViewModel> Almuerzo { get; set; }

        public List<ComidaViewModel> OpcionalMediodia { get; set; }
        public List<ComidaViewModel> MediaTarde { get; set; }
        public List<ComidaViewModel> Cena { get; set; }
        public List<ComidaViewModel> OpcionalNoche { get; set; }
        public List<ComidaViewModel> Merienda { get; set; }
    }
}