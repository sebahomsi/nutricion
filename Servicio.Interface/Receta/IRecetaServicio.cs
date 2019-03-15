﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Servicio.Interface.Alimento;

namespace Servicio.Interface.Receta
{
    public interface IRecetaServicio
    {
        Task<long> Add(RecetaDto dto);
        Task Update(RecetaDto dto);
        Task Delete(long id);
        Task<ICollection<RecetaDto>> Get(bool eliminado, string cadenaBuscar);
        Task<RecetaDto> GetById(long id);
        Task<int> GetNextCode();

        Task<ICollection<RecetaDto>> GetByFoods(ICollection<AlimentoDto> alimentos);
    }
}
