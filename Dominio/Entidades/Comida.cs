using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Comidas")]
    [MetadataType(typeof(IComida))]

    public class Comida :EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long DiaId { get; set; }

        //Propiedades de Navegacion
        public virtual Dia Dia { get; set; }
        public virtual ICollection<ComidaDetalle> ComidasDetalles { get; set; }
    }
}
