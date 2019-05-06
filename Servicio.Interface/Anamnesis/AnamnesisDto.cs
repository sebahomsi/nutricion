using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Anamnesis
{
    public class AnamnesisDto
    {
        public long Id { get; set; }

        public string Descripcion { get; set; }

        public long PacienteId { get; set; }
    }
}
