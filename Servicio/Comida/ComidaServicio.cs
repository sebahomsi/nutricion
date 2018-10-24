using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Comida;

namespace Servicio.Comida
{
    public class ComidaServicio : ServicioBase, IComidaServicio
    {
        public async Task<long> Add(ComidaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(ComidaDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<ComidaDto>> Get(string cadenaBuscar)
        {
            throw new NotImplementedException();
        }

        public async Task<ComidaDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNextCode()
        {
            throw new NotImplementedException();
        }
    }
}
