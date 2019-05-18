using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("SubGrupos")]
    [MetadataType(typeof(ISubGrupo))]

    public class SubGrupo : EntidadBase
    {
        public int Codigo { get; set; }

        public long GrupoId { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual ICollection<Alimento> Alimentos { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
