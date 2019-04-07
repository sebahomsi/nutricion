using Dominio.Entidades.MetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("PlanesAlimenticios")]
    [MetadataType(typeof(IPlanAlimenticio))]

    public class PlanAlimenticio : EntidadBase
    {
        public int Codigo { get; set; }
        public long PacienteId { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
        public bool Eliminado { get; set; }

        public int TotalCalorias { get; set; }

        //Propiedades de Navegacion
        public virtual Paciente Paciente { get; set; }
        public virtual ICollection<Dia> Dias { get; set; }
    }
}
