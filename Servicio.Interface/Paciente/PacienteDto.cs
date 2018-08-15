using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Persona;

namespace Servicio.Interface.Paciente
{
    public class PacienteDto : PersonaDto
    {
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public bool TieneAnalitico { get; set; }
        //le faltan las listas
    }
}
