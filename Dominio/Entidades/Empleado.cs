using Dominio.Entidades.MetaData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Personas_Empleado")]
    [MetadataType(typeof(IEmpleado))]

    public class Empleado : Persona
    {
        public int Legajo { get; set; }
    }
}
