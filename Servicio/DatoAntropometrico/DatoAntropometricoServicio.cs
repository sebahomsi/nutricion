using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.DatoAntropometrico;

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
            var dato = Context.DatosAntropometricos.Find(dto.Id);
            if (dato == null) throw new ArgumentNullException();

            dato.Codigo = dto.Codigo; //no se modifica
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
            var dato = Context.DatosAntropometricos.Find(id);
            if (dato == null) throw new ArgumentNullException();

            dato.Eliminado = !dato.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<DatoAntropometricoDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            DateTime.TryParse(cadenaBuscar, out var fecha);
            return await Context.DatosAntropometricos
                .AsNoTracking()
                .Where(x => x.Codigo == codigo
                || x.FechaMedicion.Date == fecha)
                .Select(x => new DatoAntropometricoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    PacienteId = x.PacienteId,
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
    }
}
