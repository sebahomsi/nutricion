using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IPaciente
    {
        [Required]
        [Index("Index_Paciente_Codigo", IsUnique = true)]
        int Codigo { get; set; }

        bool Estado { get; set; }

        bool TieneObservacion { get; set; }
    }
}
