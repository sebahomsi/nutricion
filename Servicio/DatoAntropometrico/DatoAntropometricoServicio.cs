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
            var totalPliegues = await CalculatePliegues(dto);

            var dato = new Dominio.Entidades.DatoAntropometrico()
            {
                Codigo = dto.Codigo,
                PacienteId = dto.PacienteId,
                Altura = dto.Altura,
                FechaMedicion = dto.FechaMedicion,
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
                PliegueSuprailiaco = dto.PliegueSuprailiaco,
                PliegueMuslo = dto.PliegueMuslo,
                PlieguePierna = dto.PlieguePierna,
                PliegueSubescapular = dto.PliegueSubescapular,
                PliegueTriceps = dto.PliegueTriceps,
                PliegueAbdominal = dto.PliegueAbdominal,
                TotalPliegues = totalPliegues
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
            var totalPliegues = await CalculatePliegues(dto);

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
            dato.PliegueSuprailiaco = dto.PliegueSuprailiaco;
            dato.PliegueMuslo = dto.PliegueMuslo;
            dato.PlieguePierna = dto.PlieguePierna;
            dato.PliegueSubescapular = dto.PliegueSubescapular;
            dato.PliegueTriceps = dto.PliegueTriceps;
            dato.PliegueAbdominal = dto.PliegueAbdominal;
            dato.TotalPliegues = totalPliegues;

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
                    FechaMedicion = x.FechaMedicion,
                    MasaGrasa = x.MasaGrasa,
                    MasaCorporal = x.MasaCorporal,
                    PesoActual = x.PesoActual,
                    PerimetroCintura = x.PerimetroCintura,
                    PerimetroCadera = x.PerimetroCadera,
                    PerimetroCuello = x.PerimetroCuello,
                    Eliminado = x.Eliminado,
                    PesoDeseado = x.PesoDeseado,
                    PesoHabitual = x.PesoHabitual,
                    PesoIdeal = x.PesoIdeal,
                    Foto = x.Foto,
                    PliegueSuprailiaco = x.PliegueSuprailiaco,
                    PliegueMuslo = x.PliegueMuslo,
                    PlieguePierna = x.PlieguePierna,
                    PliegueSubescapular = x.PliegueSubescapular,
                    PliegueTriceps = x.PliegueTriceps,
                    PliegueAbdominal = x.PliegueAbdominal,
                    TotalPliegues = x.TotalPliegues
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
                    PesoActual = dato.PesoActual,
                    PerimetroCintura = dato.PerimetroCintura,
                    PerimetroCadera = dato.PerimetroCadera,
                    PerimetroCuello = dato.PerimetroCuello,
                    Eliminado = dato.Eliminado,
                    PesoDeseado = dato.PesoDeseado,
                    PesoHabitual = dato.PesoHabitual,
                    PesoIdeal = dato.PesoIdeal,
                    Foto = dato.Foto,
                    PliegueSuprailiaco = dato.PliegueSuprailiaco,
                    PliegueMuslo = dato.PliegueMuslo,
                    PlieguePierna = dato.PlieguePierna,
                    PliegueSubescapular = dato.PliegueSubescapular,
                    PliegueTriceps = dato.PliegueTriceps,
                    PliegueAbdominal = dato.PliegueAbdominal,
                    TotalPliegues = dato.TotalPliegues
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
            var datos = await Context.DatosAntropometricos.Include("Paciente").Where(x => x.PacienteId == id).ToListAsync();

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
            int sexo = 1;

            if (DateTime.Now.CompareTo(nacimientoAhora) < 0)
            {
                edad--;
            }
            if (paciente.Sexo == 2)
            {
                sexo = 0;
            }

            return await Task.Run(() =>
            {
                double.TryParse(imc, out var imcDecimal);
                double.TryParse(edad.ToString(), out var edadD);
                var resultado = (imcDecimal * 1.2) + (0.23 * edadD) - (10.8 * sexo) - 5.4;
               
                return resultado.ToString("##.000");
            });
        }

        public async Task<decimal> CalculatePliegues(DatoAntropometricoDto dto)
        {
            return await Task.Run(() =>
            {
                var totalPliegues = dto.PliegueAbdominal + dto.PliegueMuslo + dto.PlieguePierna + dto.PliegueSubescapular +
                                    dto.PliegueSuprailiaco + dto.PliegueTriceps;

                return totalPliegues;
            });
        }

        public async Task Actualizar()
        {
            var lista = await Context.DatosAntropometricos.ToListAsync();

            foreach (var dato in lista)
            {
                if (!dato.Foto.Contains('|'))
                {
                    dato.Foto = dato.Foto +"|"+ "~/Content/Imagenes/user-icon.jpg";
                }
            }
            await Context.SaveChangesAsync();
        }
    }
}
