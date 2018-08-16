using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.Opcion;

namespace Servicio.Interface.Alimento
{
    public class AlimentoDto
    {
        public AlimentoDto()
        {
            MicroNutrientes = new List<MicroNutrienteDto>();
            Opciones = new List<OpcionDto>();
        }
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long SubGrupoId { get; set; }

        public string SubGrupoStr { get; set; }

        public bool EstaEliminado { get; set; }

        public long MacroNutrienteId { get; set; }

        public bool TieneMacroNutriente { get; set; }

        public List<MicroNutrienteDto> MicroNutrientes { get; set; }
        public List<OpcionDto> Opciones { get; set; }
    }
}
