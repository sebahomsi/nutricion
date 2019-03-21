using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades.MetaData
{
    public interface IPaciente
    {
        [Required]
        [Index("Index_Paciente_Codigo", IsUnique = true)]
        int Codigo { get; set; }


        bool TieneObservacion { get; set; }
    }
}
