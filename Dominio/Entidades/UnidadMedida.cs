using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("UnidadMedidas")]
    [MetadataType(typeof(IUnidadMedida))]

    public class UnidadMedida : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Abreviatura { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual ICollection<MicroNutrienteDetalle> MicroNutrienteDetalles { get; set; }
        public virtual ICollection<OpcionDetalle> OpcionDetalles { get; set; }
    }
}
