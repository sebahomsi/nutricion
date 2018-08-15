using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Persona;

namespace Servicio.Interface.Empleado
{
    public class EmpleadoDto : PersonaDto
    {
        public string Legajo { get; set; }
    }
}
