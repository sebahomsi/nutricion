using System.Collections.Generic;
using Servicio.Interface.Alimento;

namespace Servicio.Interface.Receta
{
    public class RecetaDto
    {
        public RecetaDto()
        {
            Alimentos = new List<AlimentoDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }

        public List<AlimentoDto> Alimentos { get; set; }
    }
}
