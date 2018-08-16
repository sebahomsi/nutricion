using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Observacion;

namespace Servicio.Interface.Patologia
{
    public class PatologiaDto
    {
        public PatologiaDto()
        {
            Observaciones = new List<ObservacionDto>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public List<ObservacionDto> Observaciones { get; set; }
    }
}
