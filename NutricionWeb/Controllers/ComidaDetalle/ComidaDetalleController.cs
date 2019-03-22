using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.ComidaDetalle;
using PagedList;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Opcion;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.ComidaDetalle
{
    public class ComidaDetalleController : Controller
    {
        private readonly IComidaDetalleServicio _comidaDetalleServicio;
        private readonly IOpcionServicio _opcionServicio;
        private readonly IComidaServicio _comidaServicio;

        public ComidaDetalleController(IComidaDetalleServicio comidaDetalleServicio, IOpcionServicio opcionServicio, IComidaServicio comidaServicio)
        {
            _comidaDetalleServicio = comidaDetalleServicio;
            _opcionServicio = opcionServicio;
            _comidaServicio = comidaServicio;
        }

        // GET: ComidaDetalle
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var detalles =
                await _comidaDetalleServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar)
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
        public async Task<ActionResult> Create()
        {
            return View(new ComidaDetalleABMViewModel());
        }

        // POST: ComidaDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ComidaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);
                    dto.Codigo = await _comidaDetalleServicio.GetNextCode();

                    await _comidaDetalleServicio.Add(dto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: ComidaDetalle/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: ComidaDetalle/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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

        // POST: ComidaDetalle/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(ComidaDetalleViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _comidaDetalleServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: ComidaDetalle/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
    }
}
