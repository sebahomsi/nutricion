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
    [Table("Persona_Pacientes")]
    [MetadataType(typeof(IPaciente))]

    public class Paciente : Persona
    {
        public int Codigo { get; set; }

        public bool Estado { get; set; }

        public bool TieneAnalitico { get; set; }

        //Navigation Properties
        //public virtual ICollection<DatoAntropometrico> DatosAntropometricos { get; set; }
        //public virtual ICollection<Alimento> AlimentosRechazados { get; set; }
        //public virtual ICollection<PlanAlimenticio> PlanesAlimenticios { get; set; }
        //public virtual Turno Turno { get; set; } 
    }
}
