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
                ColesterolTotal = dto.ColesterolTotal, //esto es un calculo
                PacienteId = dto.PacienteId,
                PresionDiastolica = dto.PresionDiastolica,
                PresionSistolica = dto.PresionSistolica,
                Trigliceridos = dto.Trigliceridos,
                FechaMedicion = DateTime.Now,
                Eliminado = false
            };

            Context.DatosAnaliticos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(DatoAnaliticoDto dto)
        {
            var dato = await Context.DatosAnaliticos.FirstOrDefaultAsync(x=> x.Id == dto.Id);

            if (dato == null) throw new ArgumentNullException();

            dato.ColesterolHdl = dto.ColesterolHdl;
            dato.ColesterolLdl = dto.ColesterolLdl;
            dato.ColesterolTotal = dto.ColesterolTotal; //esto es un calculo
            dato.PacienteId = dto.PacienteId;
            dato.PresionDiastolica = dto.PresionDiastolica;
            dato.PresionSistolica = dto.PresionSistolica;
            dato.Trigliceridos = dto.Trigliceridos;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var dato = await Context.DatosAnaliticos.FirstOrDefaultAsync(x => x.Id == id);

            if (dato == null) throw new ArgumentNullException();

            dato.Eliminado = !dato.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<DatoAnaliticoDto>> Get(string cadenaBuscar = "")
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.DatosAnaliticos
                .AsNoTracking()
                .Include("Paciente")
                .Select(x =>new DatoAnaliticoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    ColesterolHdl = x.ColesterolHdl,
                    ColesterolLdl = x.ColesterolLdl,
                    ColesterolTotal = x.ColesterolTotal,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    PresionDiastolica = x.PresionDiastolica,
                    PresionSistolica = x.PresionSistolica,
                    Trigliceridos = x.Trigliceridos,
                    FechaMedicion = x.FechaMedicion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<DatoAnaliticoDto> GetById(long id)
        {
            var dato = await Context.DatosAnaliticos
                .AsNoTracking()
                .Include("Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dato == null) throw new ArgumentNullException();

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
                PacienteStr = dato.Paciente.Apellido +" "+ dato.Paciente.Nombre,
                FechaMedicion = dato.FechaMedicion,
                Eliminado = dato.Eliminado
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.DatosAnaliticos.AnyAsync()
                ? await Context.DatosAnaliticos.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
