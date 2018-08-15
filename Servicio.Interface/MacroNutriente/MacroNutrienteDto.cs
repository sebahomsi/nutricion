using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.MacroNutriente
{
    public class MacroNutrienteDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public string Proteina { get; set; }

        public string Grasa { get; set; }

        public string Energia { get; set; }

        public string HidratosCarbono { get; set; }

        public bool Eliminado { get; set; }
    }
}
