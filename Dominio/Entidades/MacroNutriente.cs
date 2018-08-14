using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("MacroNutrientes")]
    [MetadataType(typeof(IMacroNutriente))]

    public class MacroNutriente : EntidadBase
    {
        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public string Proteina { get; set; }

        public string Grasa { get; set; }

        public string Energia { get; set; }

        public string HidratosCarbono { get; set; }

        public bool EstaEliminado { get; set; }

        //Navigation Properties
        public virtual Alimento Alimento { get; set; }
    }
}
