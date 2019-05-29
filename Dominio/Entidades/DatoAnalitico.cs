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

        public string Glusemia { get; set; }
        public string Insulina { get; set; }
        public string VitaminaD { get; set; }
        public string CPK { get; set; }
        public string Creatinina { get; set; }
        public string B12 { get; set; }
        public string Zinc { get; set; }
        public string Fosforo { get; set; }
        public string GlobulosRojos { get; set; }
        public string Hematocritos { get; set; }
        public string Hemoglobina { get; set; }

        public bool Eliminado { get; set; }



        //Navigation Properties
        public virtual Paciente Paciente { get; set; }
    }
}
