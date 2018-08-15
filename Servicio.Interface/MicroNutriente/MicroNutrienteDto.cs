using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;

namespace Servicio.Interface.MicroNutriente
{
    public class MicroNutrienteDto
    {
        public MicroNutrienteDto()
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
