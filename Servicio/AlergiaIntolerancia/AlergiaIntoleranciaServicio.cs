using Servicio.Interface.AlergiaIntolerancia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Xml;
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
            var alergia = Context.AlergiasIntolerancias.Find(dto.Id);
            if (alergia == null) throw new ArgumentNullException();

            alergia.Codigo = dto.Codigo; //no se modifica
            alergia.Descripcion = dto.Descripcion;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var alergia = Context.AlergiasIntolerancias.Find(id);

            if (alergia == null) throw new ArgumentNullException();

            alergia.Eliminado = !alergia.Eliminado;

            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<AlergiaIntoleranciaDto>> Get(string cadenaBuscar)
        {
            int.TryParse(cadenaBuscar, out var codigo);
            return await Context.AlergiasIntolerancias
                .AsNoTracking()
                .Where(x => x.Descripcion.Contains(cadenaBuscar) 
                || x.Codigo == codigo)
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
                .Include("Observaciones")
                .Include("Observaciones.Paciente")
                .FirstOrDefaultAsync(x => x.Id == id);

            if (alergia == null) throw new ArgumentNullException();

            return new AlergiaIntoleranciaDto()
            {
                Id = alergia.Id,
                Descripcion = alergia.Descripcion,
                Codigo = alergia.Codigo,
                Eliminado = alergia.Eliminado,
                Observaciones = alergia.Observaciones.Select(p=> new ObservacionDto
                {
                    Id = p.Id,
                    BebeAlcohol = p.BebeAlcohol,
                    CantidadSuenio = p.CantidadSuenio,
                    EstadoCivil = p.EstadoCivil,
                    Fumador = p.Fumador,
                    CantidadHijo = p.CantidadHijo,
                    TuvoHijo = p.TuvoHijo,
                    Eliminado = p.Eliminado,
                    PacienteId = p.PacienteId,
                    PacienteStr = p.Paciente.Apellido +" "+ p.Paciente.Nombre
                }).ToList()
            };
        }
    }
}
