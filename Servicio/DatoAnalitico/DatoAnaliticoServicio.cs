using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAnalitico;

namespace Servicio.DatoAnalitico
{
    public class DatoAnaliticoServicio : ServicioBase, IDatoAnaliticoServicio
    {
        public async Task<long> Add(DatoAnaliticoDto dto)
        {
            var dato = new Dominio.Entidades.DatoAnalitico()
            {
                Codigo = dto.Codigo,
                ColesterolHdl = dto.ColesterolHdl,
                ColesterolLdl = dto.ColesterolLdl,
                ColesterolTotal = dto.ColesterolTotal,
                PacienteId = dto.PacienteId,
                PresionDiastolica = dto.PresionDiastolica,
                PresionSistolica = dto.PresionSistolica,
                Trigliceridos = dto.Trigliceridos
            };

            Context.DatosAnaliticos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(DatoAnaliticoDto dto)
        {
            var dato = Context.DatosAnaliticos.Find(dto.Id);

            dato.Codigo = dto.Codigo;
            dato.ColesterolHdl = dto.ColesterolHdl;
            dato.ColesterolLdl = dto.ColesterolLdl;
            dato.ColesterolTotal = dto.ColesterolTotal;
            dato.PacienteId = dto.PacienteId;
            dato.PresionDiastolica = dto.PresionDiastolica;
            dato.PresionSistolica = dto.PresionSistolica;
            dato.Trigliceridos = dto.Trigliceridos;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<DatoAnaliticoDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.DatosAnaliticos
                .AsNoTracking()
                .Where(x => x.Codigo == codigo)
                .Select(x =>new DatoAnaliticoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    ColesterolHdl = x.ColesterolHdl,
                    ColesterolLdl = x.ColesterolLdl,
                    ColesterolTotal = x.ColesterolTotal,
                    PacienteId = x.PacienteId,
                    PresionDiastolica = x.PresionDiastolica,
                    PresionSistolica = x.PresionSistolica,
                    Trigliceridos = x.Trigliceridos
                }).ToListAsync();
        }

        public async Task<DatoAnaliticoDto> GetById(long id)
        {
            var dato = await Context.DatosAnaliticos
                .AsNoTracking()
                .Include("Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);
            return new DatoAnaliticoDto()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                ColesterolHdl = dato.ColesterolHdl,
                ColesterolLdl = dato.ColesterolLdl,
                ColesterolTotal = dato.ColesterolTotal,
                PacienteId = dato.PacienteId,
                PresionDiastolica = dato.PresionDiastolica,
                PresionSistolica = dato.PresionSistolica,
                Trigliceridos = dato.Trigliceridos,
                PacienteStr = dato.Paciente.Apellido +" "+ dato.Paciente.Nombre
            };
        }
    }
}
