using System.Collections.Generic;
using Servicio.Interface.Alimento;
using Servicio.Interface.RecetaDetalle;

namespace Servicio.Interface.Receta
{
    public class RecetaDto
    {
        public RecetaDto()
        {
            RecetasDetalles = new List<RecetaDetalleDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        public List<RecetaDetalleDto> RecetasDetalles { get; set; }
    }
}
