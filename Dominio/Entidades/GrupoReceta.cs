using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("GruposRecetas")]
    [MetadataType(typeof(IGrupoReceta))]

    public class GrupoReceta : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
    }
}
