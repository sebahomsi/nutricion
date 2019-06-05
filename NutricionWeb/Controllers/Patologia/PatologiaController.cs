﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Patologia;
using PagedList;
using Servicio.Interface.Patologia;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Patologia
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class PatologiaController : Controller
    {
        private readonly IPatologiaServicio _patologiaServicio;

        public PatologiaController(IPatologiaServicio patologiaServicio)
        {
            _patologiaServicio = patologiaServicio;
        }

        // GET: Patologia
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;
            ViewBag.Eliminado = eliminado;
            var patologias =
                await _patologiaServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (patologias == null) return RedirectToAction("Error", "Home");

            return View(patologias.Select(x=> new PatologiaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: Patologia/Create
        public async Task<ActionResult> Create()
        {
            return View(new PatologiaABMViewModel());
        }

        // POST: Patologia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PatologiaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patologiaDto = CargarDatos(vm);
                    patologiaDto.Codigo = await _patologiaServicio.GetNextCode();

                    await _patologiaServicio.Add(patologiaDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> CreateParcial(long? observacionId)
        {
            ViewBag.ObservacionId = observacionId ?? -1;
            return PartialView(new PatologiaABMViewModel());
        }

        // POST: Patologia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(PatologiaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patologiaDto = CargarDatos(vm);
                    patologiaDto.Codigo = await _patologiaServicio.GetNextCode();

                    await _patologiaServicio.Add(patologiaDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return Json(new { estado = false , vista = RenderRazorViewToString("~/View/Patologia/CreateParcial.cshtml", vm)});
            }
            return Json(new {estado = true});
        }


        // GET: Patologia/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if(id == null) return RedirectToAction("Error", "Home");

            var patologia = await _patologiaServicio.GetById(id.Value);

            return View(new PatologiaABMViewModel()
            {
                Id = patologia.Id,
                Codigo = patologia.Codigo,
                Descripcion = patologia.Descripcion,
                Eliminado = patologia.Eliminado
            });
        }

        // POST: Patologia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PatologiaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patologiaDto = CargarDatos(vm);

                    await _patologiaServicio.Update(patologiaDto);
                }

            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Patologia/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var patologia = await _patologiaServicio.GetById(id.Value);

            return View(new PatologiaViewModel()
            {
                Id = patologia.Id,
                Codigo = patologia.Codigo,
                Descripcion = patologia.Descripcion,
                Eliminado = patologia.Eliminado
            });
        }

        // POST: Patologia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PatologiaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _patologiaServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Patologia/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var patologia = await _patologiaServicio.GetById(id.Value);

            return View(new PatologiaViewModel()
            {
                Id = patologia.Id,
                Codigo = patologia.Codigo,
                Descripcion = patologia.Descripcion,
                Eliminado = patologia.Eliminado
            });
        }

        //===========================================================//
        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        private PatologiaDto CargarDatos(PatologiaABMViewModel vm)
        {
            return new PatologiaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
