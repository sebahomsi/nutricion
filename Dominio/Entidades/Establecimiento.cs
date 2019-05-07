using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Establecimientos")]
    [MetadataType(typeof(IEstablecimiento))]

    public class Establecimiento : EntidadBase
    {
        public string Nombre { get; set; }
        public string Profesional { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Telefono { get; set; }
        public string Horario { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
