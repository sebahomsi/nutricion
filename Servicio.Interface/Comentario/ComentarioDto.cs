using Servicio.Interface.Opcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Comentario
{
    public class ComentarioDto
    {
        public ComentarioDto()
        {
            Opciones = new List<OpcionDto>();
        }
        public long Id { get; set; }

        public long PlanId { get; set; }
        public long ComidaId { get; set; }

        public long DiaId { get; set; }

        public string Texto { get; set; }

        public List<OpcionDto> Opciones { get; set; }
    }
}
