using System;

namespace Servicio.Interface.Persona
{
    public class PersonaDto
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Cuit { get; set; }
        public string Mail { get; set; }
        public DateTime FechaNac { get; set; }
        public int Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Foto { get; set; }
        public bool Eliminado { get; set; }
        public long EstablecimientoId { get; set; }
    }
}
