using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Dia;

namespace Servicio.Interface.PlanAlimenticio
{
    public class PlanAlimenticioDto
    {
        public PlanAlimenticioDto()
        {
            Dias = new List<DiaDto>();
        }

        public long Id { get; set; }
        public int Codigo { get; set; }
        public long PacienteId { get; set; }
        public string PacienteStr { get; set; }
        public string Motivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Comentarios { get; set; }
        public bool Eliminado { get; set; }

        public List<DiaDto> Dias { get; set; }
    }
}
