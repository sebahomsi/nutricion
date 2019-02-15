using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Mensaje
{
    public class MensajeDto
    {
        public long Id { get; set; }
        public string EmailEmisor { get; set; }
        public string EmailReceptor { get; set; }
        public string Cuerpo { get; set; }
        public string Motivo { get; set; }
        public bool Visto { get; set; }
    }
}
