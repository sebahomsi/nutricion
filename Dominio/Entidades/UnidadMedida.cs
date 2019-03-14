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
        public virtual ICollection<RecetaDetalle> RecetasDetalles { get; set; }
    }
}
