using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Pago
{
   public class PagoDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public DateTime Fecha { get; set; }

        public string Concepto { get; set; }

        public double Monto { get; set; }

        public bool EstaEliminado { get; set; }

        public long PacienteId { get; set; }
        public string PacienteStr { get; set; }
    }
}
