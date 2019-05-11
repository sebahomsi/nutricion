using Servicio.Interface.Comida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.PlanAlimenticio
{
    public class PlanDiasDto
        
    {
        public PlanDiasDto()
        {
            Desayunos = new List<ComidaDto>();
            MediaMañana = new List<ComidaDto>();
            Almuerzo = new List<ComidaDto>();
            MediaTarde = new List<ComidaDto>();
            Cena = new List<ComidaDto>();
            Merienda = new List<ComidaDto>();

        }
        public List<ComidaDto> Desayunos { get; set; }
        public List<ComidaDto> MediaMañana { get; set; }
        public List<ComidaDto> Almuerzo { get; set; }
        public List<ComidaDto> MediaTarde { get; set; }
        public List<ComidaDto> Cena { get; set; }
        public List<ComidaDto> Merienda { get; set; }



    }
}
