using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Dia;

namespace Servicio.Dia
{
    public class DiaServicio: ServicioBase, IDiaServicio
    {
        public async Task<long> Add(DiaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(DiaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DiaDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<DiaDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextCode()
        {
            throw new NotImplementedException();
        }
    }
}
