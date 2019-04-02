using AutoMapper;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicio.PlanAlimenticio
{
    public class PlanAlimenticioServicio : ServicioBase, IPlanAlimenticioServicio
    {
        private static int CodigoPlan { get; set; }
        private static int CodigoDia { get; set; }
        private static int CodigoComida { get; set; }
        private static int CodigoComidaDetalle { get; set; }

        private readonly IDiaServicio _diaServicio;
        private readonly IComidaServicio _comidaServicio;
        private readonly IComidaDetalleServicio _comidaDetalleServicio;

        public PlanAlimenticioServicio(IDiaServicio diaServicio, IComidaServicio comidaServicio, IComidaDetalleServicio comidaDetalleServicio)
        {
            //_planAlimenticioServicio = planAlimenticioServicio;
            _diaServicio = diaServicio;
            _comidaServicio = comidaServicio;
            _comidaDetalleServicio = comidaDetalleServicio;
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
            await SetCodes();
            var planAjeno = await Context.PlanesAlimenticios
                .Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion")
                .FirstOrDefaultAsync(x => x.Id == planId);

            if (planAjeno == null) throw new ArgumentNullException();
            var planNuevo = new Dominio.Entidades.PlanAlimenticio()
            {
                Motivo = planAjeno.Motivo,
                Fecha = DateTime.Now,
                PacienteId = pacienteId,
                Codigo = GetNextCode("Plan"),
                Comentarios = planAjeno.Comentarios,
                Dias = planAjeno.Dias.Select(x => new Dominio.Entidades.Dia()
                {
                    Codigo = GetNextCode("Dia"),
                    Descripcion = x.Descripcion,                 
                    Comidas = x.Comidas.Select(c => new Dominio.Entidades.Comida()
                    {
                        Codigo = GetNextCode("Comida"),
                        Descripcion = c.Descripcion,
                        ComidasDetalles = c.ComidasDetalles.Select(cd => new Dominio.Entidades.ComidaDetalle()
                        {
                            Codigo = GetNextCode("ComidaDetalle"),
                            Comentario = cd.Comentario,
                            Eliminado = cd.Eliminado,
                            OpcionId = cd.OpcionId,
                            
                        }).ToList(),
                    }).ToList(),
                }).ToList()
            };

            var paciente =await Context.Personas.OfType<Dominio.Entidades.Paciente>().Include(x => x.PlanesAlimenticios).FirstAsync(x=>x.Id==pacienteId);
            paciente.PlanesAlimenticios.Add(planNuevo);
            await Context.SaveChangesAsync();

        }

        private async Task SetCodes()
        {
            CodigoPlan = await GetNextCode();
            CodigoDia = await _diaServicio.GetNextCode();
            CodigoComida = await _comidaServicio.GetNextCode();
            CodigoComidaDetalle = await _comidaDetalleServicio.GetNextCode();
        }

        private static int GetNextCode(string codigo)
        {
            switch (codigo)
            {
                case "Plan":
                    return CodigoPlan++;
                case "Dia":
                    return CodigoDia++;
                case "Comida":
                    return CodigoComida++;
                case "ComidaDetalle":
                    return CodigoComidaDetalle++;
                default:
                    return 0;
            }
        }

    }
}
