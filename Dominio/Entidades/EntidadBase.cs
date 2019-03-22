using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades
{
    public class EntidadBase
    {
        [Key]
        public long Id { get; set; }
    }
}
