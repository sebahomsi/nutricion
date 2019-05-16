using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
