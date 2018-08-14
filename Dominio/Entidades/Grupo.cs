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
    [Table("Grupos")]
    [MetadataType(typeof(IGrupo))]

    public class Grupo : EntidadBase
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool EstaEliminado { get; set; }

        //Navigation Properties
    }
}
