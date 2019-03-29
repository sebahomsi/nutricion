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

        public int Proteina { get; set; }

        public int Grasa { get; set; }

        public int Energia { get; set; }

        public int HidratosCarbono { get; set; }

        public int Calorias { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual Alimento Alimento { get; set; }
    }
}
