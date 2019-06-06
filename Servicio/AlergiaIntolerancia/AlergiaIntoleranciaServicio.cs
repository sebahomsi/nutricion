﻿using Servicio.Interface.AlergiaIntolerancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml;
using AutoMapper;
using Servicio.Interface.Alimento;
using Servicio.Interface.Observacion;

namespace Servicio.AlergiaIntolerancia
{
    public class AlergiaIntoleranciaServicio : ServicioBase, IAlergiaIntoleranciaServicio
    {
        public async Task<long> Add(AlergiaIntoleranciaDto dto)
        {
            var alergia = new Dominio.Entidades.AlergiaIntolerancia
            {
                Codigo = dto.Codigo,
                Descripcion = dto.Descripcion,
                Eliminado = false
            };

            Context.AlergiasIntolerancias.Add(alergia);
            await Context.SaveChangesAsync();
            return alergia.Id;
        }

        public async Task Update(AlergiaIntoleranciaDto dto)
        {
            var alergia = await Context.AlergiasIntolerancias.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (alergia == null) throw new ArgumentNullException();

            alergia.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var alergia = await Context.AlergiasIntolerancias.FirstOrDefaultAsync(x=> x.Id == id);

            if (alergia == null) throw new ArgumentNullException();

            alergia.Eliminado = !alergia.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<AlergiaIntoleranciaDto>> Get(bool eliminado, string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.AlergiaIntolerancia, bool>> expression = x => x.Eliminado == eliminado && x.Descripcion.Contains(cadenaBuscar);
            return await Context.AlergiasIntolerancias
                .AsNoTracking()
                .Where(expression)
                .Select(x => new AlergiaIntoleranciaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToListAsync();
        }

        public async Task<AlergiaIntoleranciaDto> GetById(long id)
        {
            var alergia = await Context.AlergiasIntolerancias
                .AsNoTracking()
                .Include("Observaciones.Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alergia == null) throw new ArgumentNullException();

            return new AlergiaIntoleranciaDto()
            {
                Id = alergia.Id,
                Descripcion = alergia.Descripcion,
                Codigo = alergia.Codigo,
                Eliminado = alergia.Eliminado,
                Observaciones = alergia.Observaciones.Select(x=> new ObservacionDto
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido + " " + x.Paciente.Nombre,
                    Tabaco = x.Tabaco,
                    Alcohol = x.Alcohol,
                    AntecedentesFamiliares = x.AntecedentesFamiliares,
                    ActividadFisica = x.ActividadFisica,
                    RitmoEvacuatorio = x.RitmoEvacuatorio,
                    Medicacion = x.Medicacion,
                    HorasSuenio = x.HorasSuenio,
                    Eliminado = x.Eliminado
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.AlergiasIntolerancias.AnyAsync()
                ? await Context.AlergiasIntolerancias.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<ICollection<AlergiaIntoleranciaDto>> GetbyObservacionId(bool eliminado, string cadenaBuscar, long observacionId)
        {
            var observacion = await Context.Observaciones.Include(x => x.AlergiasIntolerancias).FirstOrDefaultAsync(x => x.Id == observacionId);

            var AlergiasIntoleranciasId = observacion.AlergiasIntolerancias.Select(x => new { Id = x.Id }.Id);

            var alergias = Context.AlergiasIntolerancias.Where(x => !AlergiasIntoleranciasId.Contains(x.Id) && !x.Eliminado && x.Descripcion.Contains(cadenaBuscar));

            return Mapper.Map<ICollection<AlergiaIntoleranciaDto>>(alergias);
        }
    }
}
