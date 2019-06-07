using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Comentario")]    
    public class Comentario :EntidadBase
    {
        public long ComidaId { get; set; }

        public long PlanId { get; set; }

        public long DiaId { get; set; }

        public string Texto { get; set; }

        public ICollection<Opcion> Opciones { get; set; }
    }
}
