using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Objetivo
{
    public class ObjetivoDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
