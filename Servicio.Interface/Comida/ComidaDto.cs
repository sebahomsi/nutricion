using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Comida
{
    public class ComidaDto
    {
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long DiaId { get; set; }
        public string DiaStr { get; set; }
        //faltan listas
    }
}
