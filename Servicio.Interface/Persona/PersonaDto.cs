using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Persona
{
    public class PersonaDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Mail { get; set; }
        public string FechaNac { get; set; }
        public int Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Foto { get; set; }
        public bool Eliminado { get; set; }
    }
}
