using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Turno
{
    public class TurnoDto
    {
        public long Id { get; set; }

        public long PacienteId { get; set; }

        public string PacienteStr { get; set; }

        public int Numero { get; set; }

        public DateTime HorarioEntrada { get; set; }

        public DateTime HorarioSalida { get; set; }

        public string Motivo { get; set; }


        public bool Eliminado { get; set; }
    }
}
