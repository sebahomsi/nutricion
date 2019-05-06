using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructura.Contexto;
using Servicio.Interface.Objetivo;

namespace Servicio.Objetivo
{
    public class ObjetivoServicio : ServicioBase, IObjetivoServicio
    {
        public async Task Add(ObjetivoDto dto)
        {
            var objetivo = new Dominio.Entidades.Objetivo()
            {
                Descripcion = dto.Descripcion,
                PacienteId = dto.PacienteId
            };

            Context.Objetivos.Add(objetivo);
            await Context.SaveChangesAsync();
        }

        public async Task Update(ObjetivoDto dto)
        {
            var objetivo = Context.Objetivos.FirstOrDefault(x => x.Id == dto.Id);

            objetivo.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task<ObjetivoDto> GetByPacienteId(long id)
        {
            var objetivo = await Context.Objetivos.FirstOrDefaultAsync(x => x.PacienteId == id);

            if (objetivo == null) return new ObjetivoDto()
            {
                Descripcion = "Objetivo",
                PacienteId = id,
                Id = 0
            };

            return new ObjetivoDto()
            {
                Descripcion = objetivo.Descripcion,
                Id = objetivo.Id,
                PacienteId = objetivo.PacienteId
            };
        }
    }
}
