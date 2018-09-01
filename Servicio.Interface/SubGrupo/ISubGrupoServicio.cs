﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.SubGrupo
{
    public interface ISubGrupoServicio
    {
        Task<long> Add(SubGrupoDto dto);
        Task Update(SubGrupoDto dto);
        Task Delete(long id);
        Task<ICollection<SubGrupoDto>> Get(string cadenaBuscar);
        Task<SubGrupoDto> GetById(long id);
    }
}