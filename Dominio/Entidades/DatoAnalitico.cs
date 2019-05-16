using Dominio.Entidades.MetaData;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("DatosAnaliticos")]
    [MetadataType(typeof(IDatoAnalitico))]

    public class DatoAnalitico : EntidadBase
    {
        public int Codigo { get; set; }

        public long PacienteId { get; set; }

        public string ColesterolHdl { get; set; }

        public string ColesterolLdl { get; set; }

        public string ColesterolTotal { get; set; }

        public string PresionDiastolica { get; set; }

        public string PresionSistolica { get; set; }

        public string Trigliceridos { get; set; }

        public DateTime FechaMedicion { get; set; }

        public bool Eliminado { get; set; }

        //Navigation Properties
        public virtual Paciente Paciente { get; set; }
    }
}
