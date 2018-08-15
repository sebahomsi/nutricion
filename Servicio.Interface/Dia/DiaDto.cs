using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Dia
{
    public class DiaDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long PlanAlimenticioId { get; set; }
        public string PlanAlimenticioStr { get; set; }
        //faltan listas
    }
}
