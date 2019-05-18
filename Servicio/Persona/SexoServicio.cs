using Servicio.Interface.Persona;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicio.Persona
{
    public class SexoServicio : ISexoServicio
    {
        public async Task<IEnumerable<SexoDto>> ObtenerSexo()
        {
            return await Task.Run(()=>  new List<SexoDto>()
            {
                new SexoDto() {Codigo = 1 , Descripcion = "Masculino"},
                new SexoDto() {Codigo = 2 , Descripcion = "Femenino"}

            }.ToList());
        }
    }
}
