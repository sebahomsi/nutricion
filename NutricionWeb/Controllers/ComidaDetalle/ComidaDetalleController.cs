using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Opcion;
using PagedList;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.ComidaDetalle
{
    public class ComidaDetalleController : Controller
    {
        private readonly IComidaDetalleServicio _comidaDetalleServicio;
        private readonly IOpcionServicio _opcionServicio;
        private readonly IComidaServicio _comidaServicio;
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IDiaServicio _diaServicio;
        

        public ComidaDetalleController(IComidaDetalleServicio comidaDetalleServicio, IOpcionServicio opcionServicio, IComidaServicio comidaServicio, IPlanAlimenticioServicio planAlimenticioServicio, IDiaServicio diaServicio)
        {
            _comidaDetalleServicio = comidaDetalleServicio;
            _opcionServicio = opcionServicio;
            _comidaServicio = comidaServicio;
            _planAlimenticioServicio = planAlimenticioServicio;
            _diaServicio = diaServicio;
        }

        // GET: ComidaDetalle
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var detalles =
                await _comidaDetalleServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar)
                    ? cadenaBuscar
                    : string.Empty);

            if (detalles == null) return HttpNotFound();

            return View(detalles.Select(x => new ComidaDetalleViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Comentario = x.Comentario,
                OpcionId = x.OpcionId,
                OpcionStr = x.OpcionStr,
                ComidaId = x.ComidaId,
                ComidaStr = x.ComidaStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: ComidaDetalle/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult Create(long comidaId)
        {
            return View(new ComidaDetalleABMViewModel()
            {
                ComidaId = comidaId
            });
        }

        // POST: ComidaDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Create(ComidaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);

                    var comida = await _comidaServicio.GetById(vm.ComidaId);

                    var dia = await _diaServicio.GetById(comida.DiaId);

                    dto.Codigo = await _comidaDetalleServicio.GetNextCode();

                    await _comidaDetalleServicio.Add(dto);

                    await _planAlimenticioServicio.CalculateTotalCalories(dia.PlanAlimenticioId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Comida", new { id = vm.ComidaId });

        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> CreatePartial(long comidaId)
        {
            return View(new ComidaDetalleABMViewModel()
            {
                ComidaId = comidaId
            });
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> CreatePartialPlanOrdenado(long comidaId, long? opcionId, string opcionStr = "")
        {
            TempData["ComidaId"] = comidaId;
            return PartialView(new ComidaDetalleABMViewModel()
            {
                ComidaId = comidaId,
                OpcionId = opcionId ?? 0,
                OpcionStr = opcionStr
            });
        }
        [Authorize(Roles = "Administrador, Empleado")]
        // POST: ComidaDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePartialPlanOrdenado(ComidaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);

                    var comida = await _comidaServicio.GetById(vm.ComidaId);

                    var dia = await _diaServicio.GetById(comida.DiaId);

                    dto.Codigo = await _comidaDetalleServicio.GetNextCode();

                    await _comidaDetalleServicio.Add(dto);

                    await _planAlimenticioServicio.CalculateTotalCalories(dia.PlanAlimenticioId);

                    return RedirectToAction("ExportarPlanOrdenado", "PlanAlimenticio", new { id = dia.PlanAlimenticioId });
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

        }

        [Authorize(Roles = "Administrador, Empleado")]
        // POST: ComidaDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePartial(ComidaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);

                    var comida = await _comidaServicio.GetById(vm.ComidaId);

                    var dia = await _diaServicio.GetById(comida.DiaId);

                    dto.Codigo = await _comidaDetalleServicio.GetNextCode();

                    await _comidaDetalleServicio.Add(dto);

                    await _planAlimenticioServicio.CalculateTotalCalories(dia.PlanAlimenticioId);

                    return RedirectToAction("ExportarPlanOrdenado", "PlanAlimenticio", new { id = dia.PlanAlimenticioId });
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

        }
        [Authorize(Roles = "Administrador, Empleado")]
        // GET: ComidaDetalle/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var detalle = await _comidaDetalleServicio.GetById(id.Value);

            return View(new ComidaDetalleABMViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                Comentario = detalle.Comentario,
                OpcionId = detalle.OpcionId,
                ComidaId = detalle.ComidaId,
                OpcionStr = detalle.OpcionStr,
                ComidaStr = detalle.ComidaStr,
                Eliminado = detalle.Eliminado
            });
        }
        [Authorize(Roles = "Administrador, Empleado")]
        // POST: ComidaDetalle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ComidaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);

                    await _comidaDetalleServicio.Update(dto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: ComidaDetalle/Delete/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var detalle = await _comidaDetalleServicio.GetById(id.Value);

            var vm = new ComidaDetalleViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                Comentario = detalle.Comentario,
                OpcionId = detalle.OpcionId,
                ComidaId = detalle.ComidaId,
                OpcionStr = detalle.OpcionStr,
                ComidaStr = detalle.ComidaStr,
                Eliminado = detalle.Eliminado
            };

            return RedirectToAction("Delete");

            //return View(new ComidaDetalleViewModel()
            //{
            //    Id = detalle.Id,
            //    Codigo = detalle.Codigo,
            //    Comentario = detalle.Comentario,
            //    OpcionId = detalle.OpcionId,
            //    ComidaId = detalle.ComidaId,
            //    OpcionStr = detalle.OpcionStr,
            //    ComidaStr = detalle.ComidaStr,
            //    Eliminado = detalle.Eliminado
            //});
        }

        // POST: ComidaDetalle/Delete/5
        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]

        public async Task<ActionResult> Delete(ComidaDetalleViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //await _comidaDetalleServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Eliminar(long comidaId, long detalleId)
        {
            await _comidaDetalleServicio.Delete(detalleId);

            return RedirectToAction("Details", "Comida", new { id = comidaId });

            //return RedirectToAction("Index", "Home");
        }

        // GET: ComidaDetalle/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var detalle = await _comidaDetalleServicio.GetById(id.Value);

            return View(new ComidaDetalleViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                Comentario = detalle.Comentario,
                OpcionId = detalle.OpcionId,
                ComidaId = detalle.ComidaId,
                OpcionStr = detalle.OpcionStr,
                ComidaStr = detalle.ComidaStr,
                Eliminado = detalle.Eliminado
            });
        }

        //======================Hugo pelotudo
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> TraerOpcion(long opcionId)
        {
            var opcion = await _opcionServicio.GetById(opcionId);

            return Json(opcion, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> BuscarOpcionModal(int? page, string cadenaBuscar,long comidaId,long? subgrupoId)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            ViewBag.FilterValue = cadenaBuscar;
            ViewBag.ComidaId = comidaId;

            var opciones =
                 await _opcionServicio.Get(eliminado,comidaId, subgrupoId, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(opciones.Select(x => new OpcionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> BuscarOpcion(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            ViewBag.FilterValue = cadenaBuscar;
            var opciones =
                await _opcionServicio.Get(eliminado, null, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(opciones.Select(x => new OpcionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        [Authorize(Roles = "Administrador, Empleado")]
        private ComidaDetalleDto CargarDatos(ComidaDetalleABMViewModel vm)
        {
            return new ComidaDetalleDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Comentario = vm.Comentario,
                OpcionId = vm.OpcionId,
                ComidaId = vm.ComidaId,
                Eliminado = vm.Eliminado
            };
        }

        public async Task<ActionResult> AgregarOpcionesPlan(List<long> ids , long planId,long comidaId)
        {
            try
            {
                foreach (var id in ids)
                {
                    var cod = await _comidaDetalleServicio.GetNextCode();
                    var comidaDetalle = new ComidaDetalleDto()
                    {
                        Codigo = cod,
                        Comentario = "",
                        ComidaId = comidaId,
                        Eliminado = false,
                        OpcionId = id,
                    };
                    await _comidaDetalleServicio.Add(comidaDetalle);
                    await _planAlimenticioServicio.CalculateTotalCalories(planId);
                }
            }
            catch (Exception ex)
            {
                return Json(new { estado = false, mensaje="No se guardaron los datos intente nuevamente" });
            }

            return Json(new { estado = true });
        }
    }
}
