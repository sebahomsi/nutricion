using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("Estados")]
    [MetadataType(typeof(IEstado))]

    public class Estado : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Color { get; set; }

        public bool Eliminado { get; set; }

        //======================

        public virtual ICollection<Turno> Turno { get; set; }
    }
}
