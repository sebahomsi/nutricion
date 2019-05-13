﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Opcion;
using PagedList;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.PlanAlimenticio;
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
        public async Task<ActionResult> Create(long comidaId)
        {
            return View(new ComidaDetalleABMViewModel()
            {
                ComidaId = comidaId
            });
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

                    var comida = await _comidaServicio.GetById(vm.ComidaId);

                    var dia = await _diaServicio.GetById(comida.DiaId);

                    dto.Codigo = await _comidaDetalleServicio.GetNextCode();

                    await _comidaDetalleServicio.Add(dto);

                    await _planAlimenticioServicio.CalculateTotalCalories(dia.PlanAlimenticioId);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Comida", new {id = vm.ComidaId});

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

        public async Task<ActionResult> Eliminar(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            await _comidaDetalleServicio.Delete(id.Value);

            return RedirectToAction("Index", "Home");
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

        public async Task<ActionResult> TraerOpcion(long opcionId)
        {
            var opcion = await _opcionServicio.GetById(opcionId);

            return Json(opcion, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarOpcion(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            var opciones =
                await _opcionServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(opciones.Select(x => new OpcionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
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
