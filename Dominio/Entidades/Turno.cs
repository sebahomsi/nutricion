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
    [Table("Turnos")]
    [MetadataType(typeof(ITurno))]

    public class Turno: EntidadBase
    {
        public long PacienteId { get; set; }

        public int Numero { get; set; }

        public DateTime HorarioEntrada { get; set; }

        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }


        public bool Eliminado { get; set; }

        //Las propiedades de navegacion
        public virtual Paciente Paciente { get; set; }
    }
}
