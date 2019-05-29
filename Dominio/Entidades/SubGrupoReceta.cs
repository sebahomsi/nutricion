using System.Collections.Generic;
using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("SubGruposRecetas")]
    [MetadataType(typeof(ISubGrupoReceta))]

    public class SubGrupoReceta : EntidadBase
    {
        public int Codigo { get; set; }

        public long GrupoRecetaId { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual GrupoReceta GrupoReceta { get; set; }
        public virtual ICollection<Opcion> Opciones { get; set; }
    }
}
