using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Personas_Paciente")]
    [MetadataType(typeof(IPaciente))]

    public class Paciente : Persona
    {
        public int Codigo { get; set; }

        public bool TieneObservacion { get; set; }


        //Navigation Properties
        public virtual ICollection<DatoAntropometrico> DatosAntropometricos { get; set; }
        public virtual ICollection<DatoAnalitico> DatosAnaliticos { get; set; }
        public virtual ICollection<PlanAlimenticio> PlanesAlimenticios { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
    }
}
