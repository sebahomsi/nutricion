using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.UnidadMedida;
using PagedList;
using Servicio.Interface.UnidadMedida;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.UnidadMedida
{
    [Authorize(Roles = "Administrador")]
    public class UnidadMedidaController : Controller
    {
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;

        public UnidadMedidaController(IUnidadMedidaServicio unidadMedidaServicio)
        {
            _unidadMedidaServicio = unidadMedidaServicio;
        }

        // GET: UnidadMedida
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var unidades =
                await _unidadMedidaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (unidades == null) return HttpNotFound();

            return View(unidades.Select(x=> new UnidadMedidaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Abreviatura = x.Abreviatura,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        
        // GET: UnidadMedida/Create
        public async Task<ActionResult> Create()
        {
            return View(new UnidadMedidaABMViewModel());
        }

        // POST: UnidadMedida/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UnidadMedidaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var unidadDto = CargarDatos(vm);
                    unidadDto.Codigo = await _unidadMedidaServicio.GetNextCode();

                    await _unidadMedidaServicio.Add(unidadDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: UnidadMedida/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var unidad = await _unidadMedidaServicio.GetById(id.Value);

            return View(new UnidadMedidaABMViewModel()
            {
                Id = unidad.Id,
                Codigo = unidad.Codigo,
                Descripcion = unidad.Descripcion,
                Abreviatura = unidad.Abreviatura,
                Eliminado = unidad.Eliminado
            });
        }

        // POST: UnidadMedida/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UnidadMedidaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var unidadDto = CargarDatos(vm);

                    await _unidadMedidaServicio.Update(unidadDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: UnidadMedida/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var unidad = await _unidadMedidaServicio.GetById(id.Value);

            return View(new UnidadMedidaViewModel()
            {
                Id = unidad.Id,
                Codigo = unidad.Codigo,
                Descripcion = unidad.Descripcion,
                Abreviatura = unidad.Abreviatura,
                Eliminado = unidad.Eliminado
            });
        }

        // POST: UnidadMedida/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(UnidadMedidaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unidadMedidaServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: UnidadMedida/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var unidad = await _unidadMedidaServicio.GetById(id.Value);

            return View(new UnidadMedidaViewModel()
            {
                Id = unidad.Id,
                Codigo = unidad.Codigo,
                Descripcion = unidad.Descripcion,
                Abreviatura = unidad.Abreviatura,
                Eliminado = unidad.Eliminado
            });
        }

        //===============================================
        private UnidadMedidaDto CargarDatos(UnidadMedidaABMViewModel vm)
        {
            return new UnidadMedidaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Abreviatura = vm.Abreviatura,
                Eliminado = vm.Eliminado
            };
        }
    }
}
