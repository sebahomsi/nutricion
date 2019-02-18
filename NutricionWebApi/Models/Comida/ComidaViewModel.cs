using NutricionWebApi.Models.Opcion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NutricionWebApi.Models.Comida
{
    public class ComidaViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long DiaId { get; set; }

        [Display(Name = "Dia")]
        public string DiaStr { get; set; }

        public List<OpcionViewModel> Opciones { get; set; }
    }
}