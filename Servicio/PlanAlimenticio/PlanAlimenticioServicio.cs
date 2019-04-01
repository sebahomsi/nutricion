using AutoMapper;
using Servicio.Interface.Comida;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Servicio.Interface.ComidaDetalle;

namespace Servicio.PlanAlimenticio
{
    public class PlanAlimenticioServicio : ServicioBase, IPlanAlimenticioServicio
    {
        private readonly IDiaServicio _diaServicio;

        public PlanAlimenticioServicio(IDiaServicio diaServicio)
        {
            _diaServicio = diaServicio;
        }
        public async Task<long> Add(PlanAlimenticioDto dto)
        {
            var plan = new Dominio.Entidades.PlanAlimenticio()
            {
                Codigo = dto.Codigo,
                Fecha = DateTime.Now,
                Motivo = dto.Motivo,
                PacienteId = dto.PacienteId,
                Comentarios = dto.Comentarios,
                Eliminado = false
            };

            Context.PlanesAlimenticios.Add(plan);
            await Context.SaveChangesAsync();

            return plan.Id;
        }

        public async Task Update(PlanAlimenticioDto dto)
        {
            var plan = await Context.PlanesAlimenticios.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (plan == null) throw new ArgumentNullException();

            plan.Fecha = dto.Fecha;
            plan.Motivo = dto.Motivo;
            plan.PacienteId = dto.PacienteId;
            plan.Comentarios = dto.Comentarios;

            await Context.SaveChangesAsync();
        }

        public async Task Delete(long id)
        {
            var plan = await Context.PlanesAlimenticios.FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null) throw new ArgumentNullException();

            plan.Eliminado = !plan.Eliminado;
            await Context.SaveChangesAsync();
        }

        public async Task<ICollection<PlanAlimenticioDto>> Get(bool eliminado,string cadenaBuscar = "")
        {
            Expression<Func<Dominio.Entidades.PlanAlimenticio, bool>> expression = x => x.Eliminado == eliminado && (x.Paciente.Nombre.Contains(cadenaBuscar) || x.Paciente.Apellido.Contains(cadenaBuscar));
            return await Context.PlanesAlimenticios.AsNoTracking()
                .Include("Paciente")
                .Where(expression)
                .Select(x => new PlanAlimenticioDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Fecha = x.Fecha,
                    Motivo = x.Motivo,
                    PacienteId = x.PacienteId,
                    PacienteStr = x.Paciente.Apellido +" "+ x.Paciente.Nombre,
                    Comentarios = x.Comentarios,
                    Eliminado = x.Eliminado,
                   
                }).ToListAsync();
        }

        public async Task<PlanAlimenticioDto> GetById(long id)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion")
                .FirstOrDefaultAsync(x => x.Id == id);
            if (plan == null) throw new ArgumentNullException();

            return new PlanAlimenticioDto()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.Paciente.Apellido + " " + plan.Paciente.Nombre,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                Dias = plan.Dias.Select(x=> new DiaDto()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    PlanAlimenticioStr = x.PlanAlimenticio.Motivo,
                    Comidas = x.Comidas.Select(q => new ComidaDto()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.Dia.Descripcion,
                        ComidasDetalles = q.ComidasDetalles.Select(t=> new ComidaDetalleDto()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Comentario = t.Comentario,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.Comida.Descripcion,
                            OpcionId = t.OpcionId,
                            OpcionStr = t.Opcion.Descripcion,
                            Eliminado = t.Eliminado
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<int> GetNextCode()
        {
            return await Context.PlanesAlimenticios.AnyAsync()
                ? await Context.PlanesAlimenticios.MaxAsync(x => x.Codigo) + 1
                : 1;
        }

        public async Task<IEnumerable<PlanAlimenticioDto>> GetByIdPaciente(long id)
        {
            var datos = await Context.PlanesAlimenticios
                .Include(x=>x.Paciente)
                .Where(x => x.PacienteId == id).ToListAsync();

            var planesAlimenticios = Mapper.Map<IEnumerable<PlanAlimenticioDto>>(datos);

            return planesAlimenticios;
        }

        public async Task DuplicatePlan(long planId, long pacienteId)
        {
            var planAjeno = await GetById(planId);
            var codigo = await GetNextCode();
            var plan =new Dominio.Entidades.PlanAlimenticio()
            {
                Codigo = codigo,
                PacienteId = pacienteId,
                Comentarios=planAjeno.Comentarios,
                Fecha=DateTime.Now,
                Motivo=planAjeno.Motivo,
            };
            Context.PlanesAlimenticios.Add(plan);
            await Context.SaveChangesAsync();
            await _diaServicio.GenerarDias(plan.Id);

            var planNuevo = await Context.PlanesAlimenticios
                .Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion")
                .FirstOrDefaultAsync(x => x.Id == plan.Id);//Obtengo entidad recien creada
            var Detalles = new List<ComidaDetalleDto>();
            
            foreach (var dia in planAjeno.Dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        Detalles.Add(comidaDetalle);
                    }
                }
            }
            //sin terminar







        }
    }
}
