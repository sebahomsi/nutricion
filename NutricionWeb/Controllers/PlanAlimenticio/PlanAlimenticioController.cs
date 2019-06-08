using AutoMapper;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Dia;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using PagedList;
using Rotativa;
using Servicio.Interface.Alimento;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.PlanAlimenticio
{
    public class PlanAlimenticioController : ControllerBase
    {
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IDiaServicio _diaServicio;
        private readonly IOpcionServicio _opcionServicio;
        private readonly IAlimentoServicio _alimentoServicio;

        public PlanAlimenticioController(IPlanAlimenticioServicio planAlimenticioServicio, IPacienteServicio pacienteServicio, IDiaServicio diaServicio, IOpcionServicio opcionServicio, IAlimentoServicio alimentoServicio)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _pacienteServicio = pacienteServicio;
            _diaServicio = diaServicio;
            _opcionServicio = opcionServicio;
            _alimentoServicio = alimentoServicio;
        }

        // GET: PlanAlimenticio
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var planes =
                await _planAlimenticioServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

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
                Eliminado = x.Eliminado,
                TotalCalorias = x.TotalCalorias
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));


        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DuplicarPlan()
        {
            return View(new DuplicarViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarPlan(DuplicarViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _planAlimenticioServicio.DuplicatePlan(vm.PlanId, vm.PacienteId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        // GET: PlanAlimenticio/Create
        [Authorize(Roles = "Administrador")]
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new PlanAlimenticioABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(PlanAlimenticioABMViewModel vm)
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
                else
                {
                    return PartialView(vm);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("PlanesAlimenticiosParcial", "Paciente", new { id = vm.PacienteId });

        }

        // GET: PlanAlimenticio/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: PlanAlimenticio/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
                TotalCalorias = plan.TotalCalorias
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
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
                TotalCalorias = plan.TotalCalorias,
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
                        ComidasDetalles = q.ComidasDetalles.Select(t => new ComidaDetalleViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Comentario = t.Comentario,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            OpcionId = t.OpcionId,
                            OpcionStr = t.OpcionStr,
                            Eliminado = t.Eliminado,
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        //================================================================Metodos Especiales

        public async Task<ActionResult> ExportarPlanOrdenado(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            var comidas = await _planAlimenticioServicio.GetSortringComidas(id.Value);

            ViewBag.PlanId = id;
            ViewBag.Paciente = plan.PacienteStr;

            var comidasVm = Mapper.Map<PlanAlimenticioVistaViewModel>(comidas);

            return View(comidasVm);

            #region viejo
            //return View(new PlanAlimenticioViewModel()
            //{
            //    Id = plan.Id,
            //    Codigo = plan.Codigo,
            //    Fecha = plan.Fecha,
            //    Motivo = plan.Motivo,
            //    PacienteId = plan.PacienteId,
            //    PacienteStr = plan.PacienteStr,
            //    Comentarios = plan.Comentarios,
            //    Eliminado = plan.Eliminado,
            //    TotalCalorias = plan.TotalCalorias,
            //    Dias = plan.Dias.Select(x => new DiaViewModel()
            //    {
            //        Id = x.Id,
            //        Codigo = x.Codigo,
            //        Descripcion = x.Descripcion,
            //        PlanAlimenticioId = x.PlanAlimenticioId,
            //        Comidas = x.Comidas.Select(q => new ComidaViewModel()
            //        {
            //            Id = q.Id,
            //            Codigo = q.Codigo,
            //            Descripcion = q.Descripcion,
            //            DiaId = q.DiaId,
            //            DiaStr = q.DiaStr,
            //            ComidasDetalles = q.ComidasDetalles.Select(t => new ComidaDetalleViewModel()
            //            {
            //                Id = t.Id,
            //                Codigo = t.Codigo,
            //                Comentario = t.Comentario,
            //                ComidaId = t.ComidaId,
            //                ComidaStr = t.ComidaStr,
            //                OpcionId = t.OpcionId,
            //                OpcionStr = t.OpcionStr,
            //                Eliminado = t.Eliminado,
            //                Opcion = new OpcionViewModel()
            //                {
            //                    OpcionDetalles = t.Opcion.OpcionDetalles.Select(o => new OpcionDetalleViewModel()
            //                    {
            //                        Id = o.Id,
            //                        AlimentoId = o.AlimentoId,
            //                        AlimentoStr = o.AlimentoStr,
            //                        Cantidad = o.Cantidad,
            //                        Codigo = o.Codigo,
            //                        Eliminado = o.Eliminado,
            //                        OpcionId = o.OpcionId,
            //                        OpcionStr = o.OpcionStr,
            //                        UnidadMedidaId = o.UnidadMedidaId,
            //                        UnidadMedidaStr = o.UnidadMedidaStr
            //                    }).ToList()
            //                }
            //            }).ToList()
            //        }).ToList()
            //    }).ToList()
            //}); 
            #endregion
        }

        public async Task<ActionResult> ExportarPlanPdf(long id)
        {
            var plan = await _planAlimenticioServicio.GetById(id);

            var comidas = await _planAlimenticioServicio.GetSortringComidas(id);

            ViewBag.PlanId = id;

            ViewBag.Calorias = plan.TotalCalorias;

            var comidasVm = Mapper.Map<PlanAlimenticioVistaViewModel>(comidas);

            return View(comidasVm);
        }

        public async Task<ActionResult> GeneratePdf(long planId)
        {
            var plan = await _planAlimenticioServicio.GetById(planId);

            return new ActionAsPdf("ExportarPlanPdf", new { id = planId })
            {
                FileName = "PlanAlimenticio_" + plan.PacienteStr + ".pdf",
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
        }


        public async Task<ActionResult> TraerPaciente(long? pacienteId)
        {
            if (pacienteId == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

            return Json(paciente, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;
            var eliminado = false;
            var pacientes =
                await _pacienteServicio.Get(establecimientoId, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return RedirectToAction("Error", "Home");

            return PartialView(pacientes.Select(x => new PacienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Cuit = x.Cuit,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                FechaAlta = x.FechaAlta,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }

        public async Task<ActionResult> TraerPlan(long? planId)
        {
            if (planId == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(planId.Value);

            return Json(plan, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarPlan(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;
            var planes =
                await _planAlimenticioServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (planes == null) return RedirectToAction("Error", "Home");

            return PartialView(planes.Select(x => new PlanAlimenticioViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteStr = x.PacienteStr,
                Motivo = x.Motivo,
                Comentarios = x.Comentarios,
                Eliminado = x.Eliminado,
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
                Eliminado = vm.Eliminado,
                TotalCalorias = 0,
                ComentarioPacienteOP = vm.ComentarioPacienteOP,

            };
        }
    }
}
