using Servicio.Interface.DatoAntropometrico;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicio.DatoAntropometrico
{
    public class DatoAntropometricoServicio : ServicioBase, IDatoAntropometricoServicio
    {
        public async Task<long> Add(DatoAntropometricoDto dto)
        {
            var dato = new Dominio.Entidades.DatoAntropometrico()
            {
                Codigo = dto.Codigo,
                PacienteId = dto.PacienteId,
                Altura = dto.Altura,
                FechaMedicion = DateTime.Now,
                MasaGrasa = dto.MasaGrasa,
                MasaCorporal = dto.MasaCorporal,
                Peso = dto.Peso,
                PerimetroCintura = dto.PerimetroCintura,
                PerimetroCadera = dto.PerimetroCadera,
                Eliminado = false
            };

            Context.DatosAntropometricos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(DatoAntropometricoDto dto)
        {
            var dato = await Context.DatosAntropometricos.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (dato == null) throw new ArgumentNullException();

            dato.PacienteId = dto.PacienteId;
            dato.Altura = dto.Altura;
            dato.FechaMedicion = dto.FechaMedicion; //no se modifica
            dato.MasaGrasa = dto.MasaGrasa;
            dato.MasaCorporal = dto.MasaCorporal;
            dato.Peso = dto.Peso;
            dato.PerimetroCintura = dto.PerimetroCintura;
            dato.PerimetroCadera = dto.PerimetroCadera;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var dato = await Context.DatosAntropometricos.FirstOrDefaultAsync(x => x.Id == id);
            if (dato == null) throw new ArgumentNullException();

            dato.Eliminado = !dato.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<DatoAntropometricoDto>> Get(bool eliminado,string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.DatoAntropometrico, bool>> expression = x => x.Eliminado == eliminado && (x.Paciente.Nombre.Contains(cadenaBuscar) || x.Paciente.Apellido.Contains(cadenaBuscar));

            return await Context.DatosAntropometricos
                .AsNoTracking()
                .Include("Paciente")
                .Where(expression)
                .Select(x => new DatoAntropometricoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    Altura = x.Altura,
                    FechaMedicion = x.FechaMedicion,
                    MasaGrasa = x.MasaGrasa,
                    MasaCorporal = x.MasaCorporal,
                    Peso = x.Peso,
                    PerimetroCintura = x.PerimetroCintura,
                    PerimetroCadera = x.PerimetroCadera,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<DatoAntropometricoDto> GetById(long id)
        {
            var dato = await Context.DatosAntropometricos
                .AsNoTracking()
                .Include("Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);

            return dato == null
                ? throw new ArgumentNullException()
                : new DatoAntropometricoDto()
                {
                    Id = dato.Id,
                    Codigo = dato.Codigo,
                    PacienteId = dato.PacienteId,
                    PacienteStr = dato.Paciente.Apellido + " " + dato.Paciente.Nombre,
                    Altura = dato.Altura,
                    FechaMedicion = dato.FechaMedicion,
                    MasaGrasa = dato.MasaGrasa,
                    MasaCorporal = dato.MasaCorporal,
                    Peso = dato.Peso,
                    PerimetroCintura = dato.PerimetroCintura,
                    PerimetroCadera = dato.PerimetroCadera,
                    Eliminado = dato.Eliminado
                };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.DatosAntropometricos.AnyAsync()
                ? await Context.DatosAntropometricos.MaxAsync(x => x.Codigo) + 1
                : 1;
        }
    }
}
