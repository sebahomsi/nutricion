using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.Anamnesis;
using Servicio.Interface.Objetivo;

namespace Servicio.Anamnesis
{
    public class AnamnesisServicio : ServicioBase, IAnamnesisServicio
    {
        public async Task Add(AnamnesisDto dto)
        {
            var anamnesis = new Dominio.Entidades.Anamnesis()
            {
                Descripcion = dto.Descripcion,
                PacienteId = dto.PacienteId
            };

            Context.Anamnesis.Add(anamnesis);
            await Context.SaveChangesAsync();
        }

        public async Task Update(AnamnesisDto dto)
        {
            var anamnesis = Context.Anamnesis.FirstOrDefault(x => x.Id == dto.Id);

            anamnesis.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task<AnamnesisDto> GetByPacienteId(long id)
        {
            var anamnesis = await Context.Anamnesis.FirstOrDefaultAsync(x => x.PacienteId == id);

            if (anamnesis == null) return new AnamnesisDto()
            {
                Descripcion = "Anamnesis",
                PacienteId = id,
                Id = 0
            };

            return new AnamnesisDto()
            {
                Descripcion = anamnesis.Descripcion,
                Id = anamnesis.Id,
                PacienteId = anamnesis.PacienteId
            };
        }
    }
}
