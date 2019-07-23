using System.Collections.Generic;
using Servicio.Interface.ComidaDetalle;

namespace Servicio.Interface.Comida
{
    public class ComidaDto
    {
        public ComidaDto()
        {
            ComidasDetalles =  new List<ComidaDetalleDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long DiaId { get; set; }
        public string DiaStr { get; set; }
        public decimal SubTotalCalorias { get; set; }
             
        
        public List<ComidaDetalleDto> ComidasDetalles { get; set; }
    }
}
