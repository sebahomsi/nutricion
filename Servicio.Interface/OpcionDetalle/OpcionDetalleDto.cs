using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.OpcionDetalle
{
    public class OpcionDetalleDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public long OpcionId { get; set; }
        public string OpcionStr { get; set; }
        public long AlimentoId { get; set; }
        public string AlimentoStr { get; set; }
        public decimal Cantidad { get; set; }
        public string Unidad { get; set; }
        public bool Eliminado { get; set; }
    }
}
