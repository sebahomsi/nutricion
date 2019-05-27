using NutricionWeb.Models.Opcion;
using System.Collections.Generic;

namespace NutricionWeb.Models.SubGrupoReceta
{
    public class SubGrupoRecetaViewModel
    {
        public SubGrupoRecetaViewModel()
        {
            Opciones = new List<OpcionViewModel>();
        }

        public long Id { get; set; }

        public int Codigo { get; set; }

        public long GrupoRecetaId { get; set; }

        public string GrupoRecetaStr { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public List<OpcionViewModel> Opciones { get; set; }
    }
}