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
using Servicio.Interface.Alimento;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;

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
        private readonly IOpcionServicio _opcionServicio;
        private readonly IAlimentoServicio _alimentoServicio;

        public PlanAlimenticioServicio(IDiaServicio diaServicio, IComidaServicio comidaServicio, IComidaDetalleServicio comidaDetalleServicio, IOpcionServicio opcionServicio, IAlimentoServicio alimentoServicio)
        {
            _diaServicio = diaServicio;
            _comidaServicio = comidaServicio;
            _comidaDetalleServicio = comidaDetalleServicio;
            _opcionServicio = opcionServicio;
            _alimentoServicio = alimentoServicio;
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
                Eliminado = false,
                TotalCalorias = 0,
                ComentarioPacienteOP = string.Empty
                
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
            plan.ComentarioPacienteOP = dto.ComentarioPacienteOP;

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
                .OrderBy(x=>x.Fecha)
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
                    TotalCalorias = x.TotalCalorias,
                    ComentarioPacienteOP = x.ComentarioPacienteOP,


                }).ToListAsync();
        }

        public async Task<PlanAlimenticioDto> GetById(long id)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento")
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
                TotalCalorias = plan.TotalCalorias,
                ComentarioPacienteOP =plan.ComentarioPacienteOP,
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
                            Eliminado = t.Eliminado,
                            Opcion = new OpcionDto(){
                                OpcionDetalles = t.Opcion.OpcionDetalles.Select(o => new Interface.OpcionDetalle.OpcionDetalleDto()
                                {
                                    Id = o.Id,
                                    AlimentoId = o.AlimentoId,
                                    AlimentoStr = o.Alimento.Descripcion,
                                    Cantidad = o.Cantidad,
                                    Codigo = o.Codigo,
                                    Eliminado = o.Eliminado,
                                    OpcionId = o.OpcionId,
                                    OpcionStr = o.Opcion.Descripcion,
                                    UnidadMedidaId = o.UnidadMedidaId,
                                    UnidadMedidaStr = o.UnidadMedida.Abreviatura
                                }).ToList()
                            }
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
                .OrderByDescending(x => x.Fecha)
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
                TotalCalorias = planAjeno.TotalCalorias,
                ComentarioPacienteOP = planAjeno.ComentarioPacienteOP,
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

        public async Task CalculateTotalCalories(long plandId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == plandId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;
            var caloriasDia = 0m;
            var contadorComidas = 0;

            foreach (var dia in dias)
            {
                var flag = false;

                foreach (var comida in dia.Comidas)
                {

                    if (comida.ComidasDetalles.Any())
                    {
                        flag = true;
                    }
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        foreach (var detalle in opcion.OpcionDetalles.Where(x=>x.Eliminado == false))
                        {
                            var cantidadComidas = comida.ComidasDetalles.Count();

                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Calorias;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;                            

                                default :
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria/cantidadComidas;
                        }
                        caloriasDia += caloriasPromedioComida;
                        caloriasPromedioComida = 0;
                    }

                }
                if (flag)
                {
                    contadorComidas++;
                }
            }
            plan.TotalCalorias =  contadorComidas != 0 ? (int) caloriasDia / contadorComidas : 0;
            await Context.SaveChangesAsync();
        }

        public async Task<int> CalculateTotalCaloriesGrasas(long plandId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == plandId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;
            var caloriasDia = 0m;
            var contadorComidas = 0;

            foreach (var dia in dias)
            {
                var flag = false;

                foreach (var comida in dia.Comidas)
                {
                    if (comida.ComidasDetalles.Any())
                    {
                        flag = true;
                    }

                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x=> x.Eliminado == false))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Grasa * 9;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                        caloriasDia += caloriasPromedioComida;
                        caloriasPromedioComida = 0;
                    }
                }
                if (flag)
                {
                    contadorComidas++;
                }
            }
            return (int)caloriasDia / contadorComidas;
        }


        public async Task<int> CalculateTotalCaloriesCarbos(long plandId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == plandId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;
            var caloriasDia = 0m;
            var contadorComidas = 0;

            foreach (var dia in dias)
            {
                var flag = false;

                foreach (var comida in dia.Comidas)
                {
                    if (comida.ComidasDetalles.Any())
                    {
                        flag = true;
                    }
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x=> !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.HidratosCarbono * 4;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                        caloriasDia += caloriasPromedioComida;
                        caloriasPromedioComida = 0;
                    }
                }
                if (flag)
                {
                    contadorComidas++;
                }
            }
            return (int)caloriasDia / contadorComidas;
        }

        public async Task<int> CalculateTotalCaloriesProtes(long plandId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == plandId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;
            var caloriasDia = 0m;
            var contadorComidas = 0;

            foreach (var dia in dias)
            {
                var flag = false;

                foreach (var comida in dia.Comidas)
                {
                    if (comida.ComidasDetalles.Any())
                    {
                        flag = true;
                    }
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x=> !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Proteina * 4;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                        caloriasDia += caloriasPromedioComida;
                        caloriasPromedioComida = 0;
                    }
                }
                if (flag)
                {
                    contadorComidas++;
                }
            }
            return (int)caloriasDia / contadorComidas;
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

        public async Task<PlanDiasDto> GetSortringComidas(long PlanId)
        {
            var diasDto = new PlanDiasDto();

            var plan = await GetById(PlanId);

            foreach (var dia in plan.Dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    switch (comida.Descripcion)
                    {
                        case "Almuerzo":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id,comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.Almuerzo.Add(comida);
                            break;
                        case "Cena":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.Cena.Add(comida);
                            break;
                        case "Desayuno":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.Desayunos.Add(comida);
                            break;
                        case "Media Mañana":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.MediaMañana.Add(comida);
                            break;
                        case "Media Tarde":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.MediaTarde.Add(comida);
                            break;
                        case "Merienda":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.Merienda.Add(comida);
                            break;
                        case "Opcional Mediodia":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.OpcionalMediodia.Add(comida);
                            break;
                        case "Opcional Noche":
                            comida.SubTotalCalorias = await CalcularSubtotalCalorias(plan.Id, comida.Id);
                            comida.SubTotalCaloriasCarbo = await CalcularSubtotalCaloriasCarbo(plan.Id, comida.Id);
                            comida.SubTotalCaloriasProte = await CalcularSubtotalCaloriasProte(plan.Id, comida.Id);
                            comida.SubTotalCaloriasGrasa = await CalcularSubtotalCaloriasGrasa(plan.Id, comida.Id);
                            diasDto.OpcionalNoche.Add(comida);
                            break;
                        default:
                            break;
                    }
                }
            }
            return diasDto;
        }

        private async Task<decimal> CalcularSubtotalCalorias(long planId,long comidaId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == planId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;

            foreach (var dia in dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    if (comida.Id != comidaId) continue;
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x => !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Calorias;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                    }
                }
            }
            return caloriasPromedioComida;
        }

        private async Task<decimal> CalcularSubtotalCaloriasCarbo(long planId, long comidaId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == planId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;

            foreach (var dia in dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    if (comida.Id != comidaId) continue;
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x => !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.HidratosCarbono * 4;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                    }
                }
            }
            return caloriasPromedioComida;
        }

        private async Task<decimal> CalcularSubtotalCaloriasProte(long planId, long comidaId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == planId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;

            foreach (var dia in dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    if (comida.Id != comidaId) continue;
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x => !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Proteina * 4;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                    }
                }
            }
            return caloriasPromedioComida;
        }

        private async Task<decimal> CalcularSubtotalCaloriasGrasa(long planId, long comidaId)
        {
            var plan = await Context.PlanesAlimenticios
                .Include("Dias.Comidas.ComidasDetalles")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.UnidadMedida")
                .FirstOrDefaultAsync(x => x.Id == planId);

            if (plan == null) throw new ArgumentNullException();

            var dias = plan.Dias;
            var caloriasPromedioComida = 0m;

            foreach (var dia in dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    if (comida.Id != comidaId) continue;
                    foreach (var comidaDetalle in comida.ComidasDetalles)
                    {
                        var opcion = comidaDetalle.Opcion;

                        var cantidadComidas = comida.ComidasDetalles.Count();

                        foreach (var detalle in opcion.OpcionDetalles.Where(x => !x.Eliminado))
                        {
                            var alimento = detalle.Alimento;

                            var caloria = alimento.MacroNutriente.Grasa * 9;

                            switch (detalle.UnidadMedida.Abreviatura.ToLower())
                            {
                                case "g":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "ml":
                                    caloria = (detalle.Cantidad * caloria / 100);
                                    break;
                                case "l":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "kg":
                                    caloria = ((detalle.Cantidad * 1000) * caloria / 100);
                                    break;
                                case "cdapo":
                                    caloria = ((detalle.Cantidad * 5) * caloria / 100);
                                    break;
                                case "cdasp":
                                    caloria = ((detalle.Cantidad * 15) * caloria / 100);
                                    break;
                                case "tzTE":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "tzCAFE":
                                    caloria = ((detalle.Cantidad * 250) * caloria / 100);
                                    break;
                                case "plhon":
                                    caloria = ((detalle.Cantidad * 200) * caloria / 100);
                                    break;
                                case "plpr":
                                    caloria = ((detalle.Cantidad * 120) * caloria / 100);
                                    break;
                                case "cdate":
                                    caloria = ((detalle.Cantidad * 2) * caloria / 100);
                                    break;

                                default:
                                    throw new Exception($"La unidad de medida {detalle.UnidadMedida.Abreviatura} en el alimento {detalle.Alimento.Descripcion} no es compatible con el calculo");

                            }
                            caloriasPromedioComida += caloria / cantidadComidas;
                        }
                    }
                }
            }
            return caloriasPromedioComida;
        }

        public async Task DuplicarComidaDeOtroPlan(long? planDesdeId, long? planHastaId, string comidaDescripcion)
        {
            var planDesde = Context.PlanesAlimenticios.Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente").FirstOrDefault(p => p.Id == planDesdeId.Value);

            var planHasta = Context.PlanesAlimenticios.Include("Paciente")
                .Include("Dias.Comidas.ComidasDetalles.Opcion.OpcionDetalles.Alimento.MacroNutriente").FirstOrDefault(p => p.Id == planHastaId.Value);

            List<Dominio.Entidades.Comida> comidas = new List<Dominio.Entidades.Comida>();

            var dias = planHasta.Dias;

            var auxiliares = new List<DetalleAuxiliar>();
            foreach (var dia in planDesde.Dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    if (comida.Descripcion == comidaDescripcion)
                    {
                        var add = new DetalleAuxiliar()
                        {
                            ComidaDescripcion = comida.Descripcion,
                            Detalles = comida.ComidasDetalles.ToList(),
                            DiaDescripcion = dia.Descripcion,
                        };

                        auxiliares.Add(add);
                    }
                }
            }

            foreach (var aux in auxiliares)
            {
                foreach (var dia in planHasta.Dias)
                {
                    if (dia.Descripcion == aux.DiaDescripcion)
                    {
                        foreach (var comida in dia.Comidas)
                        {
                            if (comida.Descripcion == aux.ComidaDescripcion)
                            {
                                foreach (var detaller in aux.Detalles)
                                {
                                    if (aux.DiaDescripcion == dia.Descripcion)
                                    {
                                        var detalle = new Dominio.Entidades.ComidaDetalle()
                                        {
                                            Codigo = await _comidaDetalleServicio.GetNextCode(),
                                            Comentario = detaller.Comentario,
                                            ComidaId = comida.Id,
                                            OpcionId = detaller.OpcionId,
                                            Eliminado = false
                                        };
                                        Context.ComidasDetalles.Add(detalle);
                                        await Context.SaveChangesAsync();
                                    }                                   
                                }                                
                            }
                        }
                    }
                }
            }

            await Context.SaveChangesAsync();

        }

        public async Task<List<ComidaDetalleDto>> GetFoodsByPlanId(long id)
        {
            var plan = await GetById(id);
            var comidas = new List<ComidaDetalleDto>();

            foreach (var  dia in plan.Dias)
            {
                foreach (var comida in dia.Comidas)
                {
                    foreach (var detalle in comida.ComidasDetalles)
                    {
                        if (comidas.All(x => x.OpcionStr != detalle.OpcionStr))
                        {
                            comidas.Add(detalle);
                        }
                    }
                }
            }

            return comidas;
        }

        public class DetalleAuxiliar
        {
            public string ComidaDescripcion { get; set; }

            public string DiaDescripcion { get; set; }

            public IList<Dominio.Entidades.ComidaDetalle> Detalles { get; set; }

            

        }
    }
}
