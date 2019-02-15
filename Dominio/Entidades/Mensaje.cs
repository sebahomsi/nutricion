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
    [Table("Mensajes")]
    [MetadataType(typeof(IMensaje))]

    public class Mensaje : EntidadBase
    {
        public string EmailEmisor { get; set; }
        public string EmailReceptor { get; set; }
        public string Cuerpo { get; set; }
        public string Motivo { get; set; }
        public bool Visto { get; set; }
    }
}
