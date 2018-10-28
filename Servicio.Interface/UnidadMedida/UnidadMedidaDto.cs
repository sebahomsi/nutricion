using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.OpcionDetalle;

namespace Servicio.Interface.UnidadMedida
{
    public class UnidadMedidaDto
    {
        public UnidadMedidaDto()
        {
            MicroNutrienteDetalles = new List<MicroNutrienteDetalleDto>();
            OpcionDetalles = new List<OpcionDetalleDto>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Abreviatura { get; set; }

        public bool Eliminado { get; set; }

        public List<MicroNutrienteDetalleDto> MicroNutrienteDetalles { get; set; }
        public List<OpcionDetalleDto> OpcionDetalles { get; set; }
    }
}
