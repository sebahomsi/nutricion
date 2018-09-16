using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.MicroNutriente;

namespace Servicio.Interface.MicroNutrienteDetalle
{
    public interface IMicroNutrienteDetalleServicio
    {
        Task<long> Add(MicroNutrienteDetalleDto dto);
        Task Update(MicroNutrienteDetalleDto dto);
        Task Delete(long id);
        Task<ICollection<MicroNutrienteDetalleDto>> Get(string cadenaBuscar);
        Task<MicroNutrienteDetalleDto> GetById(long id);
        Task<int> GetNextCode();
    }
}
