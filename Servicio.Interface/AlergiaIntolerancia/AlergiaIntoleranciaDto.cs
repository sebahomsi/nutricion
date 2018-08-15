using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.AlergiaIntolerancia
{
    public class AlergiaIntoleranciaDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Eliminado { get; set; }
        //faltan listas
    }
}
