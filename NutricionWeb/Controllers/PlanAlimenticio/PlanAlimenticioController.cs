using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.Dia;
using NutricionWeb.Models.Opcion;
using NutricionWeb.Models.OpcionDetalle;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using PagedList;
using RazorPDF;
using Rotativa;
using Servicio.Interface.Comida;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.PlanAlimenticio
{
    public class PlanAlimenticioController : Controller
    {
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IDiaServicio _diaServicio;

        public PlanAlimenticioController(IPlanAlimenticioServicio planAlimenticioServicio, IPacienteServicio pacienteServicio, IDiaServicio diaServicio)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _pacienteServicio = pacienteServicio;
            _diaServicio = diaServicio;
        }

        // GET: PlanAlimenticio
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var planes =
                await _planAlimenticioServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (planes == null) return HttpNotFound();

            return View(planes.Select(x => new PlanAlimenticioViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Fecha = x.Fecha,
                Motivo = x.Motivo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Comentarios = x.Comentarios,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: PlanAlimenticio/Create
        public async Task<ActionResult> Create()
        {
            return View(new PlanAlimenticioABMViewModel());
        }

        // POST: PlanAlimenticio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    planDto.Codigo = await _planAlimenticioServicio.GetNextCode();

                    var planId = await _planAlimenticioServicio.Add(planDto);
                    await _diaServicio.GenerarDias(planId);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: PlanAlimenticio/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioABMViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado
            });
        }

        // POST: PlanAlimenticio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    await _planAlimenticioServicio.Update(planDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: PlanAlimenticio/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado
            });
        }

        // POST: PlanAlimenticio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PlanAlimenticioViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _planAlimenticioServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        // GET: PlanAlimenticio/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                Dias = plan.Dias.Select(x=> new DiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    Comidas = x.Comidas.Select(q => new ComidaViewModel()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.DiaStr,
                        Opciones = q.Opciones.Select(t => new OpcionViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Descripcion = t.Descripcion,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            Eliminado = t.Eliminado,
                            OpcionDetalles = t.OpcionDetalles.Select(r => new OpcionDetalleViewModel()
                            {
                                Id = r.Id,
                                Codigo = r.Codigo,
                                AlimentoId = r.AlimentoId,
                                AlimentoStr = r.AlimentoStr,
                                Cantidad = r.Cantidad,
                                OpcionId = r.OpcionId,
                                OpcionStr = r.OpcionStr,
                                UnidadMedidaId = r.UnidadMedidaId,
                                UnidadMedidaStr = r.UnidadMedidaStr,
                                Eliminado = r.Eliminado
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        //================================================================Metodos Especiales

        public async Task<ActionResult> ExportarPlan(long id)
        {
            var plan = await _planAlimenticioServicio.GetById(id);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                Dias = plan.Dias.Select(x => new DiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    Comidas = x.Comidas.Select(q => new ComidaViewModel()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.DiaStr,
                        Opciones = q.Opciones.Select(t => new OpcionViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Descripcion = t.Descripcion,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            Eliminado = t.Eliminado,
                            OpcionDetalles = t.OpcionDetalles.Select(r => new OpcionDetalleViewModel()
                            {
                                Id = r.Id,
                                Codigo = r.Codigo,
                                AlimentoId = r.AlimentoId,
                                AlimentoStr = r.AlimentoStr,
                                Cantidad = r.Cantidad,
                                OpcionId = r.OpcionId,
                                OpcionStr = r.OpcionStr,
                                UnidadMedidaId = r.UnidadMedidaId,
                                UnidadMedidaStr = r.UnidadMedidaStr,
                                Eliminado = r.Eliminado
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        public async Task<ActionResult> ExportarPlanPdf(long id)
        {
            var plan = await _planAlimenticioServicio.GetById(id);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                Dias = plan.Dias.Select(x => new DiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    Comidas = x.Comidas.Select(q => new ComidaViewModel()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.DiaStr,
                        Opciones = q.Opciones.Select(t => new OpcionViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Descripcion = t.Descripcion,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            Eliminado = t.Eliminado,
                            OpcionDetalles = t.OpcionDetalles.Select(r => new OpcionDetalleViewModel()
                            {
                                Id = r.Id,
                                Codigo = r.Codigo,
                                AlimentoId = r.AlimentoId,
                                AlimentoStr = r.AlimentoStr,
                                Cantidad = r.Cantidad,
                                OpcionId = r.OpcionId,
                                OpcionStr = r.OpcionStr,
                                UnidadMedidaId = r.UnidadMedidaId,
                                UnidadMedidaStr = r.UnidadMedidaStr,
                                Eliminado = r.Eliminado
                            }).ToList()
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        public ActionResult GeneratePdf(long planId)
        {
            return new ActionAsPdf("ExportarPlanPdf", new {id = planId})
            {
                FileName = "PlanAlimenticio" + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
        }


        public async Task<ActionResult> TraerPaciente(long? pacienteId)
        {
            if (pacienteId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

            return Json(paciente, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1; 

            var pacientes =
                await _pacienteServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return HttpNotFound(); 

            return PartialView(pacientes.Select(x => new PacienteViewModel() 
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
                Estado = x.Estado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        private PlanAlimenticioDto CargarDatos(PlanAlimenticioABMViewModel vm)
        {
            return new PlanAlimenticioDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Fecha = vm.Fecha,
                Motivo = vm.Motivo,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Comentarios = vm.Comentarios,
                Eliminado = vm.Eliminado
            };
        }
    }
}
