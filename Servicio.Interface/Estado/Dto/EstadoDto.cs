using Servicio.Interface.Turno;
using System.Collections.Generic;

namespace Servicio.Interface.Estado.Dto
{
    public class EstadoDto
    {
        public EstadoDto()
        {
            Turnos = new List<TurnoDto>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public string Color { get; set; }

        public bool Eliminado { get; set; }

        public List<TurnoDto> Turnos { get; set; }
    }
}
