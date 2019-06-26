using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Turno;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicio.Paciente
{
    public class PacienteServicio : ServicioBase, IPacienteServicio
    {
        public async Task<long> Add(PacienteDto dto)
        {
            var (id, mensaje) = await VerifyDuplicity(dto);

            if (id.HasValue) throw new ArgumentException(mensaje);

            var paciente = new Dominio.Entidades.Paciente()
            {
                Codigo = dto.Codigo,
                Apellido = dto.Apellido,
                Nombre = dto.Nombre,
                Celular = dto.Celular,
                Dni = dto.Dni,
                Cuit = dto.Cuit,
                Mail = dto.Mail,
                Telefono = dto.Telefono,
                Sexo = dto.Sexo,
                FechaNac = dto.FechaNac,
                FechaAlta = dto.FechaAlta,
                Foto = dto.Foto,
                Eliminado = false,
                TieneObservacion = false,
                EstablecimientoId = dto.EstablecimientoId
            };

            Context.Personas.Add(paciente);
            await Context.SaveChangesAsync();
            return paciente.Id;
        }

        public async Task Update(PacienteDto dto)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>().FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (paciente == null) throw new ArgumentNullException();

            if (paciente.Mail != dto.Mail)
            {
                var (id, mensaje) = await VerifyDuplicity(dto);

                if (id.HasValue) throw new ArgumentException(mensaje);
            }
            paciente.Apellido = dto.Apellido;
            paciente.Nombre = dto.Nombre;
            paciente.Celular = dto.Celular;
            paciente.Dni = dto.Dni;
            paciente.Cuit = dto.Cuit;
            paciente.Mail = dto.Mail;
            paciente.Telefono = dto.Telefono;
            paciente.Sexo = dto.Sexo;
            paciente.FechaNac = dto.FechaNac;
            paciente.Foto = dto.Foto;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>().FirstOrDefaultAsync(x => x.Id == id);
            if (paciente == null) throw new ArgumentNullException();

            paciente.Eliminado = !paciente.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PacienteDto>> Get(long? establecimientoId, bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Paciente, bool>> expression =
                x => x.Eliminado == eliminado
            && (x.Apellido.Contains(cadenaBuscar)
            || x.Nombre.Contains(cadenaBuscar)) && (!establecimientoId.HasValue || x.EstablecimientoId == establecimientoId);
            return await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .AsNoTracking()
                .Where(expression)
                .Select(x => new PacienteDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Apellido = x.Apellido,
                    Nombre = x.Nombre,
                    Celular = x.Celular,
                    Dni = x.Dni,
                    Cuit = x.Cuit,
                    Mail = x.Mail,
                    Telefono = x.Telefono,
                    Sexo = x.Sexo,
                    FechaNac = x.FechaNac,
                    FechaAlta = x.FechaAlta,
                    Foto = x.Foto,
                    Eliminado = x.Eliminado,
                }).ToListAsync();
        }

        public async Task<PacienteDto> GetById(long id)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .AsNoTracking()
                .Include("DatosAnaliticos")
                .Include("DatosAntropometricos")
                .Include("PlanesAlimenticios")
                .Include("Turnos")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (paciente == null) throw new ArgumentNullException();

            return new PacienteDto()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Dni = paciente.Dni,
                Cuit = paciente.Cuit,
                Mail = paciente.Mail,
                Telefono = paciente.Telefono,
                Sexo = paciente.Sexo,
                FechaNac = paciente.FechaNac,
                FechaAlta = paciente.FechaAlta,
                Foto = paciente.Foto,
                Eliminado = paciente.Eliminado,
                TieneObservacion = paciente.TieneObservacion,
                DatosAntropometricos = paciente.DatosAntropometricos.Select(p => new DatoAntropometricoDto()
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    PacienteId = p.PacienteId,
                    PacienteStr = p.Paciente.Apellido + " " + p.Paciente.Nombre,
                    Altura = p.Altura,
                    FechaMedicion = p.FechaMedicion,
                    MasaGrasa = p.MasaGrasa,
                    MasaCorporal = p.MasaCorporal,
                    PesoActual = p.PesoActual,
                    PerimetroCintura = p.PerimetroCintura,
                    PerimetroCadera = p.PerimetroCadera,
                    PesoHabitual = p.PesoHabitual,
                    Eliminado = p.Eliminado,
                    PesoIdeal = p.PesoIdeal,
                    PesoDeseado = p.PesoDeseado,
                    PerimetroCuello = p.PerimetroCuello

                }).ToList(),
                PlanesAlimenticios = paciente.PlanesAlimenticios.OrderBy(q => q.Fecha).Select(q => new PlanAlimenticioDto()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Motivo = q.Motivo,
                    Fecha = q.Fecha,
                    PacienteId = q.PacienteId,
                    PacienteStr = q.Paciente.Apellido + " " + q.Paciente.Nombre,
                    Eliminado = q.Eliminado,
                    TotalCalorias = q.TotalCalorias
                }).ToList(),
                Turnos = paciente.Turnos.OrderBy(t => t.HorarioEntrada).Where(t => t.HorarioEntrada >= DateTime.Today).Select(t => new TurnoDto()
                {
                    Id = t.Id,
                    Numero = t.Numero,
                    Motivo = t.Motivo,
                    PacienteId = t.PacienteId,
                    PacienteStr = t.Paciente.Apellido + " " + t.Paciente.Nombre,
                    HorarioEntrada = t.HorarioEntrada,
                    HorarioSalida = t.HorarioSalida,
                    Eliminado = t.Eliminado
                }).ToList(),
                DatosAnaliticos = paciente.DatosAnaliticos.OrderBy(x => x.Codigo).Select(x => new DatoAnaliticoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    ColesterolHdl = x.ColesterolHdl,
                    ColesterolLdl = x.ColesterolLdl,
                    ColesterolTotal = x.ColesterolTotal,
                    PresionDiastolica = x.PresionDiastolica,
                    PresionSistolica = x.PresionSistolica,
                    Trigliceridos = x.Trigliceridos,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido + " " + x.Paciente.Nombre,
                    FechaMedicion = x.FechaMedicion,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<PacienteDto> GetByEmail(string email)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .AsNoTracking()
                .Include("DatosAnaliticos")
                .Include("DatosAntropometricos")
                .Include("PlanesAlimenticios")
                .Include("Turnos")
                .FirstOrDefaultAsync(x => x.Mail == email);
            if (paciente == null) return null;

            return new PacienteDto()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Dni = paciente.Dni,
                Cuit = paciente.Cuit,
                Mail = paciente.Mail,
                Telefono = paciente.Telefono,
                Sexo = paciente.Sexo,
                FechaNac = paciente.FechaNac,
                FechaAlta = paciente.FechaAlta,
                Foto = paciente.Foto,
                Eliminado = paciente.Eliminado,
                TieneObservacion = paciente.TieneObservacion,
                DatosAntropometricos = paciente.DatosAntropometricos.Select(p => new DatoAntropometricoDto()
                {
                    Id = p.Id,
                    Codigo = p.Codigo,
                    PacienteId = p.PacienteId,
                    PacienteStr = p.Paciente.Apellido + " " + p.Paciente.Nombre,
                    Altura = p.Altura,
                    FechaMedicion = p.FechaMedicion,
                    MasaGrasa = p.MasaGrasa,
                    MasaCorporal = p.MasaCorporal,
                    PesoActual = p.PesoActual,
                    PerimetroCintura = p.PerimetroCintura,
                    PerimetroCadera = p.PerimetroCadera,
                    PesoHabitual = p.PesoHabitual,
                    Eliminado = p.Eliminado,
                    PesoIdeal = p.PesoIdeal,
                    PesoDeseado = p.PesoDeseado,
                    PerimetroCuello = p.PerimetroCuello,

                }).ToList(),
                PlanesAlimenticios = paciente.PlanesAlimenticios.OrderBy(q => q.Fecha).Select(q => new PlanAlimenticioDto()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Motivo = q.Motivo,
                    Fecha = q.Fecha,
                    PacienteId = q.PacienteId,
                    PacienteStr = q.Paciente.Apellido + " " + q.Paciente.Nombre,
                    Eliminado = q.Eliminado
                }).ToList(),
                Turnos = paciente.Turnos.OrderBy(t => t.HorarioEntrada).Where(t => t.HorarioEntrada >= DateTime.Today).Select(t => new TurnoDto()
                {
                    Id = t.Id,
                    Numero = t.Numero,
                    Motivo = t.Motivo,
                    PacienteId = t.PacienteId,
                    PacienteStr = t.Paciente.Apellido + " " + t.Paciente.Nombre,
                    HorarioEntrada = t.HorarioEntrada,
                    HorarioSalida = t.HorarioSalida,
                    Eliminado = t.Eliminado
                }).ToList(),
                DatosAnaliticos = paciente.DatosAnaliticos.OrderBy(x => x.Codigo).Select(x => new DatoAnaliticoDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    ColesterolHdl = x.ColesterolHdl,
                    ColesterolLdl = x.ColesterolLdl,
                    ColesterolTotal = x.ColesterolTotal,
                    PresionDiastolica = x.PresionDiastolica,
                    PresionSistolica = x.PresionSistolica,
                    Trigliceridos = x.Trigliceridos,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido + " " + x.Paciente.Nombre,
                    FechaMedicion = x.FechaMedicion,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Personas.OfType<Dominio.Entidades.Paciente>().AnyAsync()
                ? await Context.Personas.OfType<Dominio.Entidades.Paciente>().MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<(long? Id, string Mensaje)> VerifyDuplicity(PacienteDto dto)
        {
            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .FirstOrDefaultAsync(x => x.Mail == dto.Mail && x.Id!=dto.Id);

            if (paciente != null) { return (paciente.Id, Mensaje: "Ya existe un paciente con ese Mail"); }

            paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .FirstOrDefaultAsync(x => x.Dni == dto.Dni && x.Id!=dto.Id);

            if (paciente != null) { return  (paciente.Id, Mensaje: "Ya existe un paciente con ese Dni"); }

            return (null, Mensaje: "");
        }

    }
}
