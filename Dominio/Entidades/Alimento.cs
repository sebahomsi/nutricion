using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Alimentos")]
    [MetadataType(typeof(IAlimento))]

    public class Alimento : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long SubGrupoId { get; set; }

        public bool Eliminado { get; set; }


        //Navigation Properties
        public virtual ICollection<MicroNutrienteDetalle> MicroNutrienteDetalles { get; set; }
        public virtual ICollection<Observacion> Observaciones { get; set; }
        public virtual SubGrupo SubGrupo { get; set; }
        public virtual MacroNutriente MacroNutriente { get; set; }
        public virtual ICollection<Opcion> Opciones { get; set; }
    }
}
