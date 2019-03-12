using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.RecetaAlimento;
using Servicio.Interface.Alimento;
using Servicio.Interface.Receta;
using Servicio.Interface.RecetaAlimento;

namespace NutricionWeb.Controllers.Receta
{
    public class RecetaAlimentoController : Controller
    {
        private readonly IRecetaAlimentoServicio _recetaAlimentoServicio;

        private readonly IAlimentoServicio _alimentoServicio;

        public RecetaAlimentoController(IRecetaAlimentoServicio recetaAlimentoServicio, IAlimentoServicio alimentoServicio)
        {
            _recetaAlimentoServicio = recetaAlimentoServicio;
            _alimentoServicio = alimentoServicio;
        }

        // GET: RecetaAlimento
        public ActionResult Index()
        {
            return View();
        }

        // GET: RecetaAlimento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObservacionAlimento/Create
        public async Task<ActionResult> Create(long? recetaId)
        {
            return View(new RecetaAlimentoViewModel()
            {
                RecetaId = recetaId.Value
            });
        }

        // POST: ObservacionAlimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecetaAlimentoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _recetaAlimentoServicio.Add(vm.RecetaId, vm.AlimentoId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Receta", new { id = vm.RecetaId });
        }

        // GET: RecetaAlimento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RecetaAlimento/Edit/5
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

        // GET: /Delete/5
        public async Task<ActionResult> Delete(long recetaId, long alimentoId)
        {
            await _recetaAlimentoServicio.Delete(recetaId, alimentoId);
            return RedirectToAction("Details", "Receta", new { id = recetaId });
        }

        // POST: RecetaAlimento/Delete/5
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
    }
}
