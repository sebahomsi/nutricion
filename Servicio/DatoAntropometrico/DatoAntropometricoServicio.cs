using Servicio.Interface.DatoAntropometrico;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;

namespace Servicio.DatoAntropometrico
{
    public class DatoAntropometricoServicio : ServicioBase, IDatoAntropometricoServicio
    {
        public async Task<long> Add(DatoAntropometricoDto dto)
        {
            var imc = await CalculateImc(dto.PesoActual, dto.Altura);
            var pgc = await CalculatePgc(imc, dto.PacienteId);

            var dato = new Dominio.Entidades.DatoAntropometrico()
            {
                Codigo = dto.Codigo,
                PacienteId = dto.PacienteId,
                Altura = dto.Altura,
                FechaMedicion = DateTime.Now,
                MasaGrasa = pgc,
                MasaCorporal = imc,
                PesoActual = dto.PesoActual,
                PerimetroCintura = dto.PerimetroCintura,
                PerimetroCadera = dto.PerimetroCadera,
                PerimetroCuello = dto.PerimetroCuello,
                Eliminado = false,
                PesoDeseado = dto.PesoDeseado,
                PesoHabitual = dto.PesoHabitual,
                PesoIdeal = dto.PesoIdeal,
                Foto = dto.Foto,
            };

            Context.DatosAntropometricos.Add(dato);
            await Context.SaveChangesAsync();
            return dato.Id;
        }

        public async Task Update(DatoAntropometricoDto dto)
        {
            var dato = await Context.DatosAntropometricos.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (dato == null) throw new ArgumentNullException();
            var imc = await CalculateImc(dto.PesoActual, dto.Altura);
            var pgc = await CalculatePgc(imc, dto.PacienteId);

            dato.PacienteId = dto.PacienteId;
            dato.Altura = dto.Altura;
            dato.FechaMedicion = dto.FechaMedicion; //no se modifica
            dato.MasaGrasa = pgc;
            dato.MasaCorporal = imc;
            dato.PesoActual = dto.PesoActual;
            dato.PerimetroCintura = dto.PerimetroCintura;
            dato.PerimetroCadera = dto.PerimetroCadera;
            dato.PerimetroCuello = dto.PerimetroCuello;
            dato.PesoDeseado = dto.PesoDeseado;
            dato.PesoHabitual = dto.PesoHabitual;
            dato.PesoIdeal = dto.PesoIdeal;
            dato.Foto = dto.Foto;

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
                    PacienteStr = x.Paciente.Apellido + " " + x.Paciente.Nombre,
                    Altura = x.Altura,
                    FechaMedicion = DateTime.Now,
                    MasaGrasa = x.MasaGrasa,
                    MasaCorporal = x.MasaCorporal,
                    PesoActual = x.PesoActual,
                    PerimetroCintura = x.PerimetroCintura,
                    PerimetroCadera = x.PerimetroCadera,
                    PerimetroCuello = x.PerimetroCuello,
                    Eliminado = false,
                    PesoDeseado = x.PesoDeseado,
                    PesoHabitual = x.PesoHabitual,
                    PesoIdeal = x.PesoIdeal,
                    Foto = x.Foto,
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
                    FechaMedicion = DateTime.Now,
                    MasaGrasa = dato.MasaGrasa,
                    MasaCorporal = dato.MasaCorporal,
                    PesoActual = dato.PesoActual,
                    PerimetroCintura = dato.PerimetroCintura,
                    PerimetroCadera = dato.PerimetroCadera,
                    PerimetroCuello = dato.PerimetroCuello,
                    Eliminado = false,
                    PesoDeseado = dato.PesoDeseado,
                    PesoHabitual = dato.PesoHabitual,
                    PesoIdeal = dato.PesoIdeal,
                    Foto = dato.Foto,
                };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.DatosAntropometricos.AnyAsync()
                ? await Context.DatosAntropometricos.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<IEnumerable<DatoAntropometricoDto>> GetByIdPaciente(long id)
        {
            var datos = await Context.DatosAntropometricos.Where(x => x.PacienteId == id).ToListAsync();

            var datosAntopometricos = Mapper.Map<IEnumerable<DatoAntropometricoDto>>(datos);

            return datosAntopometricos;
        }

        public async Task<string> CalculateImc(string peso, string altura)
        {         
            return await Task.Run(() =>
            {
                double.TryParse(peso, out var pesoDecimal);
                double.TryParse(altura, out var alturaDecimal);
                var alturaM = alturaDecimal / 100;
                var resultado = pesoDecimal / (alturaM * alturaM);
                return resultado.ToString("##.00");
            });           
        }

        public async Task<string> CalculatePgc(string imc, long pacienteId)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .FirstOrDefaultAsync(x => x.Id == pacienteId);

            if (paciente == null) throw new ArgumentNullException($"No se encontro el paciente");

            var edad = DateTime.Now.Year - paciente.FechaNac.Date.Year;

            var nacimientoAhora = paciente.FechaNac.Date.AddYears(edad);

            if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
            {
                edad--;
            }

            return await Task.Run(() =>
            {
                double.TryParse(imc, out var imcDecimal);
                double.TryParse(edad.ToString(), out var edadD);
                var resultado = (imcDecimal * 1.2) + (0.23 * edadD) - (10.8 * paciente.Sexo) - 5.4;
               
                return resultado.ToString("##.000");
            });
        }
    }
}
