using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.MicroNutriente;
using PagedList;
using Servicio.Interface.MicroNutriente;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.MicroNutriente
{
    public class MicroNutrienteController : Controller
    {
        private readonly IMicroNutrienteServicio _microNutrienteServicio;

        public MicroNutrienteController(IMicroNutrienteServicio microNutrienteServicio)
        {
            _microNutrienteServicio = microNutrienteServicio;
        }
        // GET: MicroNutriente
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var micros =
                await _microNutrienteServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (micros == null) return HttpNotFound();

            return View(micros.Select(x=> new MicroNutrienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        
        // GET: MicroNutriente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MicroNutriente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MicroNutriente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MicroNutriente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MicroNutriente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MicroNutriente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MicroNutriente/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
