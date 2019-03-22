using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("ComidasDetalles")]
    [MetadataType(typeof(IComidaDetalle))]

    public class ComidaDetalle : EntidadBase
    {
        public int Codigo { get; set; }
        public string Comentario { get; set; }
        public long OpcionId { get; set; }
        public long ComidaId { get; set; }
        public bool Eliminado { get; set; }

        public virtual Opcion Opcion { get; set; }
        public virtual Comida Comida { get; set; }
    }
}
