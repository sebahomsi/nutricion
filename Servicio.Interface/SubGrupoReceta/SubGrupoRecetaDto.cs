using Servicio.Interface.Opcion;
using System.Collections.Generic;

namespace Servicio.Interface.SubGrupoReceta
{
    public class SubGrupoRecetaDto
    {
        public SubGrupoRecetaDto()
        {
            Opciones = new List<OpcionDto>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public long GrupoRecetaId { get; set; }

        public string GrupoRecetaStr { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public List<OpcionDto> Opciones { get; set; }
    }
}
