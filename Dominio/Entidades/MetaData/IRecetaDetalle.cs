using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades.MetaData
{
    public interface IRecetaDetalle
    {
        int Codigo { get; set; }
        long RecetaId { get; set; }
        long AlimentoId { get; set; }
        long UnidadMedidaId { get; set; }
        decimal Cantidad { get; set; }
        bool Eliminado { get; set; }
    }
}
