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
    [Table("Observaciones")]
    [MetadataType(typeof(IObservacion))]

    public class Observacion : EntidadBase
    {
        public long PacienteId { get; set; }

        public bool Fumador { get; set; }

        public bool BebeAlcohol { get; set; }

        public string EstadoCivil { get; set; }

        public bool? TuvoHijo { get; set; }

        public string CantidadHijo { get; set; }

        public string CantidadSuenio { get; set; }
        
        public bool Eliminado { get; set; }

        //Propiedades de Navegacion
        public virtual Paciente Paciente { get; set; }
        public virtual ICollection<AlergiaIntolerancia> AlergiasIntolerancias { get; set; }
        public virtual ICollection<Patologia> Patologias { get; set; }
    }
}
