using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IMensaje
    {
        string EmailEmisor { get; set; }
        string EmailReceptor { get; set; }

        [Required(ErrorMessage = "Campo Obligatorio")]
        string Cuerpo { get; set; }
        string Motivo { get; set; }
        bool Visto { get; set; }
    }
}
