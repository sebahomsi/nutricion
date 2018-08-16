using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Comida;

namespace Servicio.Interface.Dia
{
    public class DiaDto
    {
        public DiaDto()
        {
            Comidas = new List<ComidaDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long PlanAlimenticioId { get; set; }
        public string PlanAlimenticioStr { get; set; }
        
        public List<ComidaDto> Comidas { get; set; }
    }
}
