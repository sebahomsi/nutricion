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
    [Table("Opciones")]
    [MetadataType(typeof(IOpcion))]

    public class Opcion : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long ComidaId { get; set; }
        public bool Eliminado { get; set; }

        //Propiedades de Navegacion
        public virtual Comida Comida { get; set; }
        public virtual ICollection<OpcionDetalle> OpcionDetalles { get; set; }
    }
}
