using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("MicroNutrientes")]
    [MetadataType(typeof(IMicroNutriente))]

    public class MicroNutriente : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual ICollection<MicroNutrienteDetalle> MicroNutrienteDetalles { get; set; }
    }
}
