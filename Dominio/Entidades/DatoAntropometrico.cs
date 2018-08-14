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
    [Table("DatosAntropometricos")]
    [MetadataType(typeof(IDatoAntropometrico))]

    public class DatoAntropometrico : EntidadBase
    {
        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string Peso { get; set; }

        public string Altura { get; set; }

        public string PerimetroCadera { get; set; }

        public string PerimetroCintura { get; set; }

        public DateTime FechaMedicion { get; set; }

        public string MasaGrasa { get; set; }

        public string MasaCorporal { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual Paciente Paciente { get; set; }
    }
}
