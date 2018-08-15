using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.MicroNutrienteDetalle
{
    public class MicroNutrienteDetalleDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public long AlimentoId { get; set; }

        public string AlimentoStr { get; set; }

        public long MicroNutrienteId { get; set; }

        public string MicroNutrienteStr { get; set; }

        public double Cantidad { get; set; }

        public string Unidad { get; set; }
    }
}
