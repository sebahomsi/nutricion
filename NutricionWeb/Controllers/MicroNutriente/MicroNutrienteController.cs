using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.MicroNutriente;
using PagedList;
using Servicio.Interface.MicroNutriente;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.MicroNutriente
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class MicroNutrienteController : Controller
    {
        private readonly IMicroNutrienteServicio _microNutrienteServicio;

        public MicroNutrienteController(IMicroNutrienteServicio microNutrienteServicio)
        {
            _microNutrienteServicio = microNutrienteServicio;
        }
        // GET: MicroNutriente
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var micros =
                await _microNutrienteServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (micros == null) return RedirectToAction("Error", "Home");

            return View(micros.Select(x=> new MicroNutrienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        
        // GET: MicroNutriente/Create
        public async Task<ActionResult> Create()
        {
            return View(new MicroNutrienteABMViewModel());
        }

        // POST: MicroNutriente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MicroNutrienteABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var microDto = CargarDatos(vm);
                    microDto.Codigo = await _microNutrienteServicio.GetNextCode();

                    await _microNutrienteServicio.Add(microDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");

        }

        // GET: MicroNutriente/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if(id == null) return RedirectToAction("Error", "Home");

            var micro = await _microNutrienteServicio.GetById(id.Value);

            return View(new MicroNutrienteABMViewModel()
            {
                Id = micro.Id,
                Codigo = micro.Codigo,
                Descripcion = micro.Descripcion,
                Eliminado = micro.Eliminado
            });
        }

        // POST: MicroNutriente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MicroNutrienteABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var microDto = CargarDatos(vm);

                    await _microNutrienteServicio.Update(microDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: MicroNutriente/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if(id == null) return RedirectToAction("Error", "Home");

            var micro = await _microNutrienteServicio.GetById(id.Value);

            return View(new MicroNutrienteViewModel()
            {
                Id = micro.Id,
                Codigo = micro.Codigo,
                Descripcion = micro.Descripcion,
                Eliminado = micro.Eliminado
            });
        }

        // POST: MicroNutriente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(MicroNutrienteViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _microNutrienteServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: MicroNutriente/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var micro = await _microNutrienteServicio.GetById(id.Value);

            return View(new MicroNutrienteViewModel()
            {
                Id = micro.Id,
                Codigo = micro.Codigo,
                Descripcion = micro.Descripcion,
                Eliminado = micro.Eliminado
            });
        }

        //==============================================
        private MicroNutrienteDto CargarDatos(MicroNutrienteABMViewModel vm)
        {
            return new MicroNutrienteDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
