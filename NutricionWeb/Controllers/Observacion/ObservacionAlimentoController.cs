using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.ObservacionAlimento;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.ObservacionAlimento;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Observacion
{
    [Authorize(Roles = "Administrador")]
    public class ObservacionAlimentoController : Controller
    {
        private readonly IObservacionAlimentoServicio _observacionAlimentoServicio;
        private readonly IAlimentoServicio _alimentoServicio;

        public ObservacionAlimentoController(IObservacionAlimentoServicio observacionAlimentoServicio, IAlimentoServicio alimentoServicio)
        {
            _observacionAlimentoServicio = observacionAlimentoServicio;
            _alimentoServicio = alimentoServicio;
        }
        // GET: ObservacionAlimento
        public ActionResult Index()
        {
            return View();
        }

        // GET: ObservacionAlimento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObservacionAlimento/Create
        public async Task<ActionResult> Create(long? observacionId)
        {
            return View(new ObservacionAlimentoABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }

        // POST: ObservacionAlimento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObservacionAlimentoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionAlimentoServicio.Add(vm.ObservacionId, vm.AlimentoId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Observacion", new { id = vm.ObservacionId });
        }

        // GET: ObservacionAlimento/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObservacionAlimento/Edit/5
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

        // GET: ObservacionAlimento/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long alimentoId)
        {
            await _observacionAlimentoServicio.Delete(observacionId, alimentoId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }

        // POST: ObservacionAlimento/Delete/5
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

        //===========================Metodos especiales
        public async Task<ActionResult> BuscarAlimento(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;
            var alimentos =
                await _alimentoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(alimentos.Select(x => new AlimentoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> TraerAlimento(long alimentoId)
        {
            var alimento = await _alimentoServicio.GetById(alimentoId);

            return Json(alimento, JsonRequestBehavior.AllowGet);
        }
    }
}
