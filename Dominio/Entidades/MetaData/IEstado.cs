using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades.MetaData
{
    public interface IEstado
    {
        [Required]
        [Index("Index_Estado_Codigo", IsUnique = true)]
        int Codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(100, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Campo Obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los {1} caracteres.")]
        string Color { get; set; }

        bool Eliminado { get; set; }
    }
}
