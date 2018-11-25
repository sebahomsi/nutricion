using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Grupo;
using PagedList;
using Servicio.Empleado;
using Servicio.Interface.Grupo;
using static NutricionWeb.Helpers.PagedList;
using static NutricionWeb.Helpers.File;

namespace NutricionWeb.Controllers.Grupo
{
    public class GrupoController : Controller
    {
        private readonly IGrupoServicio _grupoServicio;

        public GrupoController(IGrupoServicio grupoServicio)
        {
            _grupoServicio = grupoServicio;
        }

        // GET: Grupo
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var grupos = await _grupoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (grupos == null) return HttpNotFound();
           
            return View(grupos.Select(x=>new GrupoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Grupo/Create
        public async Task<ActionResult> Create()
        {
            return View(new GrupoABMViewModel());
        }

        // POST: Grupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GrupoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var grupoDto = CargarDatos(vm);
                    grupoDto.Codigo = await _grupoServicio.GetNextCode();

                    await _grupoServicio.Add(grupoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoABMViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        // POST: Grupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GrupoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var grupoDto = CargarDatos(vm);

                    await _grupoServicio.Update(grupoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        // POST: Grupo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(GrupoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _grupoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        //======================================================================//
        private GrupoDto CargarDatos(GrupoABMViewModel vm)
        {
            return new GrupoDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
