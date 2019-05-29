using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidades.MetaData
{
    public interface ISubGrupoReceta
    {
        int Codigo { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        long GrupoRecetaId { get; set; }

        [Required(ErrorMessage = "El campo {0} es Obligatorio")]
        [StringLength(30, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Descripcion { get; set; }

        bool Eliminado { get; set; }
    }
}
