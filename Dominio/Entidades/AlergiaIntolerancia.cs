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
    [Table("AlergiasIntolerancias")]
    [MetadataType(typeof(IAlergiaIntolerancia))]

    public class AlergiaIntolerancia : EntidadBase
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        //Propiedades de Navegacion
        //public virtual ICollection<Observacion> Observaciones { get; set; }
    }
}
