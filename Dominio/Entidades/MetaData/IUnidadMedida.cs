using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IUnidadMedida
    {
        int Codigo { get; set; }
        
        [Required(ErrorMessage = "Campo Requerido")]
        string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        string Abreviatura { get; set; }
        
        bool Eliminado { get; set; }
    }
}
