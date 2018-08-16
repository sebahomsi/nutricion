using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Opcion;

namespace Servicio.Interface.Comida
{
    public class ComidaDto
    {
        public ComidaDto()
        {
            Opciones =  new List<OpcionDto>();
        }
        public long Id { get; set; }
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public long DiaId { get; set; }
        public string DiaStr { get; set; }
        
        public List<OpcionDto> Opciones { get; set; }
    }
}
