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
    [Table("Personas_Empleado")]
    [MetadataType(typeof(IEmpleado))]

    public class Empleado : Persona
    {
        public int Legajo { get; set; }
    }
}
