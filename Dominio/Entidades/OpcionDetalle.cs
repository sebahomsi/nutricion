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
    [Table("OpcionesDetalles")]
    [MetadataType(typeof(IOpcionDetalle))]

    public class OpcionDetalle : EntidadBase
    {
        public int Codigo { get; set; }
        public long OpcionId { get; set; }
        public long AlimentoId { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public bool Eliminado { get; set; }

        //Propiedades de Navegation
        public virtual Alimento Alimento { get; set; }
        public virtual Opcion Opcion { get; set; }
    }
}
