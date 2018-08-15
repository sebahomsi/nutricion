using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Alimento
{
    public class AlimentoDto
    {
        public long Id { get; set; }

        public int Codigo { get; set; }

        public string Descripcion { get; set; }

        public long SubGrupoId { get; set; }

        public string SubGrupoStr { get; set; }

        public bool EstaEliminado { get; set; }

        public long MacroNutrienteId { get; set; }

        public bool TieneMacroNutriente { get; set; }
        //faltan listas
    }
}
