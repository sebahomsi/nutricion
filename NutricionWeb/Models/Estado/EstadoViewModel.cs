using System.Collections.Generic;
using NutricionWeb.Models.Turno;

namespace NutricionWeb.Models.Estado
{
    public class EstadoViewModel
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Color { get; set; }

        public bool Eliminado { get; set; }

        public List<TurnoViewModel> Turnos { get; set; }
    }
}