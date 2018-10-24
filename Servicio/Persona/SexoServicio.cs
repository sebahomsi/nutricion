using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Persona;

namespace Servicio.Persona
{
    public class SexoServicio: ISexoServicio
    {
        public async Task<IEnumerable<SexoDto>> ObtenerSexo()
        {
            return new List<SexoDto>()
            {
                new SexoDto() {Codigo = 1 , Descripcion = "Masculino"},
                new SexoDto() {Codigo = 2 , Descripcion = "Femenino"}
            }.ToList();
        }
    }
}
