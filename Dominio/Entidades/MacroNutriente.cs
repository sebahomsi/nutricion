using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("MacroNutrientes")]
    [MetadataType(typeof(IMacroNutriente))]

    public class MacroNutriente : EntidadBase
    {
        public decimal Proteina { get; set; }

        public decimal Grasa { get; set; }

        public decimal Energia { get; set; }

        public decimal HidratosCarbono { get; set; }

        public decimal Calorias { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual Alimento Alimento { get; set; }
    }
}
