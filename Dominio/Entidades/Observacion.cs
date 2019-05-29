using Dominio.Entidades.MetaData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Observaciones")]
    [MetadataType(typeof(IObservacion))]

    public class Observacion : EntidadBase
    {
        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string ActividadFisica { get; set; }

        public string AntecedentesFamiliares { get; set; }

        public string Tabaco { get; set; }

        public string Alcohol { get; set; }

        public string Medicacion { get; set; }

        public string HorasSuenio { get; set; }

        public string RitmoEvacuatorio { get; set; }

        public bool Eliminado { get; set; }

        //Propiedades de Navegacion
        public virtual Paciente Paciente { get; set; }
        public virtual ICollection<AlergiaIntolerancia> AlergiasIntolerancias { get; set; }
        public virtual ICollection<Alimento> Alimentos { get; set; }
        public virtual ICollection<Patologia> Patologias { get; set; }
    }
}
