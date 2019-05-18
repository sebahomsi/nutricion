using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Patologias")]
    [MetadataType(typeof(IPatologia))]

    public class Patologia : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Propiedades de Navegacion
        public virtual ICollection<Observacion> Observaciones { get; set; }
    }
}
