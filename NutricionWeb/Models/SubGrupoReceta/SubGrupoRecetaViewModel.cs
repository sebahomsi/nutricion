using NutricionWeb.Models.Opcion;
using System.Collections.Generic;
using System.ComponentModel;

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

        [DisplayName("Grpupo de Receta")]
        public string GrupoRecetaStr { get; set; }

        public string Descripcion { get; set; }

        public bool Eliminado { get; set; }

        public List<OpcionViewModel> Opciones { get; set; }
    }
}