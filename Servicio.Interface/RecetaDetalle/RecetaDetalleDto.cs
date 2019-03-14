using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Receta;

namespace Servicio.Interface.RecetaDetalle
{
    public class RecetaDetalleDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public long RecetaId { get; set; }
        public long AlimentoId { get; set; }
        public long UnidadMedidaId { get; set; }
        public decimal Cantidad { get; set; }
        public bool Eliminado { get; set; }

        public string RecetaStr { get; set; }
        public string AlimentoStr { get; set; }
        public string UnidadMedidaStr { get; set; }
    }
}
