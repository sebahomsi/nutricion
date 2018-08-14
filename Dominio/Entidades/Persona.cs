using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades.MetaData;

namespace Dominio.Entidades
{
    [Table("Personas")]
    [MetadataType(typeof(IPersona))]

    public abstract class Persona : EntidadBase
    {
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
