using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades.MetaData
{
    public interface ISubGrupoReceta
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long GrupoRecetaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        string Descripcion { get; set; }

        bool Eliminado { get; set; }
    }
}
