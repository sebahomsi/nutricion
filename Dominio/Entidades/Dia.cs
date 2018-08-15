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
    [Table("Dias")]
    [MetadataType(typeof(IDia))]

    public class Dia : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long PlanAlimenticioId { get; set; }

        //Navigation Properties
        public virtual PlanAlimenticio PlanAlimenticio { get; set; }
        public virtual ICollection<Comida> Comidas { get; set; }
    }
}
