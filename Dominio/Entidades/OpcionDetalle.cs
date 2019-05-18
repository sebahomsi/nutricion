using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("OpcionesDetalles")]
    [MetadataType(typeof(IOpcionDetalle))]

    public class OpcionDetalle : EntidadBase
    {
        public int Codigo { get; set; }
        public long OpcionId { get; set; }
        public long AlimentoId { get; set; }
        public double Cantidad { get; set; }
        public long UnidadMedidaId { get; set; }
        public bool Eliminado { get; set; }

        //Propiedades de Navegation
        public virtual Alimento Alimento { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual Opcion Opcion { get; set; }
    }
}
