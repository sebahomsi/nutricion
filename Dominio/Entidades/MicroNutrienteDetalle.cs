using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("MicroNutrienteDetalles")]
    [MetadataType(typeof(IMicroNutrienteDetalle))]

    public class MicroNutrienteDetalle : EntidadBase
    {
        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public long MicroNutrienteId { get; set; }

        public double Cantidad { get; set; }

        public long UnidadMedidaId { get; set; }

        //Propiedades de navegacion
        public virtual Alimento Alimento { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual MicroNutriente MicroNutriente { get; set; }
    }
}
