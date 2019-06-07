using Servicio.Interface.Opcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicio.Interface.Comentario
{
    public interface IComentarioServicio
    {
        Task Add(List<OpcionDto> opciones,ComentarioDto dto);
        Task Update(ComentarioDto dto);
        Task Delete(long id);
        Task<ComentarioDto> GetById(long Id);
        Task<ComentarioDto> GetSpecific(long planId, long diaId, long comidaId,long opcionId);

    }
}
