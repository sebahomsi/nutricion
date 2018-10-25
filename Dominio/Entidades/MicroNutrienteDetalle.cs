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
    [Table("MicroNutrienteDetalles")]
    [MetadataType(typeof(IMicroNutrienteDetalle))]

    public class MicroNutrienteDetalle : EntidadBase
    {
        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public long MicroNutrienteId { get; set; }

        public double Cantidad { get; set; }

        public long UnidadMedidaId { get; set; }

        //Propiedades de navegacion
        public virtual Alimento Alimento { get; set; }
        public virtual UnidadMedida UnidadMedida { get; set; }
        public virtual MicroNutriente MicroNutriente { get; set; }
    }
}
