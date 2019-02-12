using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.AlergiaIntolerancia;
using PagedList;
using Servicio.Interface.AlergiaIntolerancia;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.AlergiaIntolerancia
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class AlergiaIntoleranciaController : Controller
    {
        private readonly IAlergiaIntoleranciaServicio _alergiaIntoleranciaServicio;

        public AlergiaIntoleranciaController(IAlergiaIntoleranciaServicio alergiaIntoleranciaServicio)
        {
            _alergiaIntoleranciaServicio = alergiaIntoleranciaServicio;
        }

        // GET: AlergiaIntolerancia
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var alergiasIntolerancias =
                await _alergiaIntoleranciaServicio.Get(
                    !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (alergiasIntolerancias == null) return HttpNotFound();

            return View(alergiasIntolerancias.Select(x=> new AlergiaIntoleranciaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: AlergiaIntolerancia/Create
        public async Task<ActionResult> Create()
        {
            return View(new AlergiaIntoleranciaABMViewModel());
        }

        // POST: AlergiaIntolerancia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlergiaIntoleranciaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var alergiaIntoleranciaDto = CargarDatos(vm);
                    alergiaIntoleranciaDto.Codigo = await _alergiaIntoleranciaServicio.GetNextCode();

                    await _alergiaIntoleranciaServicio.Add(alergiaIntoleranciaDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: AlergiaIntolerancia/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var alergia = await _alergiaIntoleranciaServicio.GetById(id.Value);

            return View(new AlergiaIntoleranciaABMViewModel()
            {
                Id = alergia.Id,
                Codigo = alergia.Codigo,
                Descripcion = alergia.Descripcion,
                Eliminado = alergia.Eliminado
            });
        }

        // POST: AlergiaIntolerancia/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AlergiaIntoleranciaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var alergia = CargarDatos(vm);

                    await _alergiaIntoleranciaServicio.Update(alergia);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: AlergiaIntolerancia/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var alergia = await _alergiaIntoleranciaServicio.GetById(id.Value);

            return View(new AlergiaIntoleranciaViewModel()
            {
                Id = alergia.Id,
                Codigo = alergia.Codigo,
                Descripcion = alergia.Descripcion,
                Eliminado = alergia.Eliminado
            });
        }

        // POST: AlergiaIntolerancia/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AlergiaIntoleranciaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _alergiaIntoleranciaServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: AlergiaIntolerancia/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var alergia = await _alergiaIntoleranciaServicio.GetById(id.Value);

            return View(new AlergiaIntoleranciaViewModel()
            {
                Id = alergia.Id,
                Codigo = alergia.Codigo,
                Descripcion = alergia.Descripcion,
                Eliminado = alergia.Eliminado
            });
        }

        //==============================================//
        private AlergiaIntoleranciaDto CargarDatos(AlergiaIntoleranciaABMViewModel vm)
        {
            return new AlergiaIntoleranciaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
