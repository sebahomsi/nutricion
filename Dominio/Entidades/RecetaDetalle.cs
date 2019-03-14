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
    [Table("RecetasDetalles")]
    [MetadataType(typeof(IRecetaDetalle))]
    public class RecetaDetalle: EntidadBase
    {
        public int Codigo { get; set; }
        public long RecetaId { get; set; }
        public long AlimentoId { get; set; }
        public long UnidadMedidaId { get; set; }
        public decimal Cantidad { get; set; }
        public bool Eliminado { get; set; }

        //Navegacion
        public virtual Receta Receta { get; set; }
        public virtual Alimento Alimento { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
    }
}
