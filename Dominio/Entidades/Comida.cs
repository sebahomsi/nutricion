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
    [Table("Comidas")]
    [MetadataType(typeof(IComida))]

    public class Comida :EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long DiaId { get; set; }

        //Propiedades de Navegacion
        public virtual Dia Dia { get; set; }
        public virtual ICollection<Opcion> Opciones { get; set; }
    }
}
