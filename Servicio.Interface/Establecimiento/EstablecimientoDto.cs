using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Establecimiento
{
    public class EstablecimientoDto
    {
        public long Id { get; set; }

        public string Nombre { get; set; }
        public string Profesional { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Telefono { get; set; }
        public string Horario { get; set; }
    }
}
