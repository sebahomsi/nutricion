using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.Interface.Opcion
{
    public class OpcionDto
    {
        public OpcionDto()
        {
            OpcionDetalles = new List<OpcionDetalleDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long ComidaId { get; set; }
        public string ComidaStr { get; set; }
        public bool Eliminado { get; set; }
        
        public List<OpcionDetalleDto> OpcionDetalles { get; set; }
    }
}
