using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Grupo;

namespace Servicio.Interface.SubGrupo
{
    public class SubGrupoDto
    {
        public SubGrupoDto()
        {
            //Alimentos = new List<AlimentoDto>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public long GrupoId { get; set; }

        public string GrupoStr { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        //public List<AlimentoDto> Alimentos { get; set; }
    }
}
