using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.Alimento;
using Servicio.Interface.Observacion;
using Servicio.Interface.Patologia;

namespace Servicio.Observacion
{
    public class ObservacionServicio : ServicioBase, IObservacionServicio
    {
        public async Task<long> Add(ObservacionDto dto)
        {
            var observacion = new Dominio.Entidades.Observacion()
            {
                Codigo = dto.Codigo,
                PacienteId = dto.PacienteId,
                Fumador = dto.Fumador,
                BebeAlcohol = dto.BebeAlcohol,
                EstadoCivil = dto.EstadoCivil,
                CantidadSuenio = dto.CantidadSuenio,
                TuvoHijo = dto.TuvoHijo,
                CantidadHijo = dto.CantidadHijo,
                Eliminado = false
            };

            var paciente = await Context.Personas.OfType<Dominio.Entidades.Paciente>()
                .FirstOrDefaultAsync(x => x.Id == dto.PacienteId);
            paciente.TieneObservacion = true;

            Context.Observaciones.Add(observacion);
            await Context.SaveChangesAsync();

            return observacion.Id;
        }

        public async Task Update(ObservacionDto dto)
        {
            var observacion = await Context.Observaciones.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (observacion == null) throw new ArgumentNullException();

            observacion.PacienteId = dto.PacienteId;
            observacion.Fumador = dto.Fumador;
            observacion.BebeAlcohol = dto.BebeAlcohol;
            observacion.EstadoCivil = dto.EstadoCivil;
            observacion.CantidadSuenio = dto.CantidadSuenio;
            observacion.TuvoHijo = dto.TuvoHijo;
            observacion.CantidadHijo = dto.CantidadHijo;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var observacion = await Context.Observaciones.FirstOrDefaultAsync(x => x.Id == id);
            if (observacion == null) throw new ArgumentNullException();

            observacion.Eliminado =! observacion.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<ObservacionDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.Observacion, bool>> expression = x => x.Eliminado == eliminado && (x.Paciente.Nombre.Contains(cadenaBuscar) || x.Paciente.Apellido.Contains(cadenaBuscar));
            return await Context.Observaciones
                .AsNoTracking()
                .Include("Paciente")
                .Where(expression)
                .Select(x => new ObservacionDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    Fumador = x.Fumador,
                    BebeAlcohol = x.BebeAlcohol,
                    EstadoCivil = x.EstadoCivil,
                    CantidadSuenio = x.CantidadSuenio,
                    TuvoHijo = x.TuvoHijo,
                    CantidadHijo = x.CantidadHijo,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<ObservacionDto> GetById(long id)
        {
            var observacion = await Context.Observaciones
                .AsNoTracking()
                .Include("Paciente")
                .Include("Patologias")
                .Include("AlergiasIntolerancias")
                .Include("Alimentos")
                .FirstOrDefaultAsync(x => x.Id == id);
            if(observacion == null) throw new ArgumentNullException();

            return new ObservacionDto()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.Paciente.Apellido + " " + observacion.Paciente.Nombre,
                Fumador = observacion.Fumador,
                BebeAlcohol = observacion.BebeAlcohol,
                EstadoCivil = observacion.EstadoCivil,
                CantidadSuenio = observacion.CantidadSuenio,
                TuvoHijo = observacion.TuvoHijo,
                CantidadHijo = observacion.CantidadHijo,
                Eliminado = observacion.Eliminado,
                Patologias = observacion.Patologias.Select(x=> new PatologiaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList(),
                AlergiasIntolerancias = observacion.AlergiasIntolerancias.Select(q=> new AlergiaIntoleranciaDto()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Descripcion = q.Descripcion,
                    Eliminado = q.Eliminado
                }).ToList(),
                Alimentos = observacion.Alimentos.Select(t=> new AlimentoDto()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion,
                    Eliminado = t.Eliminado
                }).ToList()
            };
        }

        public async Task<ObservacionDto> GetByPacienteId(long id)
        {
            var observacion = await Context.Observaciones
                .AsNoTracking()
                .Include("Paciente")
                .Include("Patologias")
                .Include("AlergiasIntolerancias")
                .Include("Alimentos")
                .FirstOrDefaultAsync(x => x.PacienteId == id);
            if (observacion == null) throw new ArgumentNullException();

            return new ObservacionDto()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.Paciente.Apellido + " " + observacion.Paciente.Nombre,
                Fumador = observacion.Fumador,
                BebeAlcohol = observacion.BebeAlcohol,
                EstadoCivil = observacion.EstadoCivil,
                CantidadSuenio = observacion.CantidadSuenio,
                TuvoHijo = observacion.TuvoHijo,
                CantidadHijo = observacion.CantidadHijo,
                Eliminado = observacion.Eliminado,
                Patologias = observacion.Patologias.Select(x => new PatologiaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList(),
                AlergiasIntolerancias = observacion.AlergiasIntolerancias.Select(q => new AlergiaIntoleranciaDto()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Descripcion = q.Descripcion,
                    Eliminado = q.Eliminado
                }).ToList(),
                Alimentos = observacion.Alimentos.Select(t => new AlimentoDto()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion,
                    Eliminado = t.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.Observaciones.AnyAsync() ? await Context.Observaciones.MaxAsync(x => x.Codigo) + 1 : 1;
        }
    }
}
