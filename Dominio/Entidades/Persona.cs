using Dominio.Entidades.MetaData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Personas")]
    [MetadataType(typeof(IPersona))]

    public abstract class Persona : EntidadBase
    {
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

        public virtual Establecimiento Establecimiento { get; set; }

    }
}
