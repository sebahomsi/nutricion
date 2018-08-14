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
    [Table("MicroNutrientes")]
    [MetadataType(typeof(IMicroNutriente))]

    public class MicroNutriente : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual ICollection<Alimento> Alimentos { get; set; }
    }
}
