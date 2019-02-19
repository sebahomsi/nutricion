using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.ObservacionAlergiaIntolerancia;
using NutricionWeb.Models.ObservacionPatologia;
using PagedList;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.ObservacionAlergiaIntolerancia;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Observacion
{
    [Authorize(Roles = "Administrador")]
    public class ObservacionAlergiaIntoleranciaController : Controller
    {
        private readonly IObservacionAlergiaIntoleranciaServicio _observacionAlergiaIntoleranciaServicio;
        private readonly IAlergiaIntoleranciaServicio _alergiaIntoleranciaServicio;

        public ObservacionAlergiaIntoleranciaController(IObservacionAlergiaIntoleranciaServicio observacionAlergiaIntoleranciaServicio, IAlergiaIntoleranciaServicio alergiaIntoleranciaServicio)
        {
            _observacionAlergiaIntoleranciaServicio = observacionAlergiaIntoleranciaServicio;
            _alergiaIntoleranciaServicio = alergiaIntoleranciaServicio;
        }
        // GET: ObservacionAlergiaIntolerancia
        public ActionResult Index()
        {
            return View();
        }

        // GET: ObservacionAlergiaIntolerancia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObservacionPatologia/Create
        public async Task<ActionResult> Create(long? observacionId)
        {
            return View(new ObservacionAlergiaIntoleranciaABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }

        // POST: ObservacionPatologia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObservacionAlergiaIntoleranciaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionAlergiaIntoleranciaServicio.Add(vm.ObservacionId, vm.AlergiaId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Observacion", new { id = vm.ObservacionId });
        }

        // GET: ObservacionAlergiaIntolerancia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObservacionAlergiaIntolerancia/Edit/5
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

        // GET: ObservacionAlergiaIntolerancia/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long alergiaId)
        {
            await _observacionAlergiaIntoleranciaServicio.Delete(observacionId, alergiaId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }

        // POST: ObservacionAlergiaIntolerancia/Delete/5
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
        public async Task<ActionResult> BuscarAlergia(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var alergias =
                await _alergiaIntoleranciaServicio.Get(false,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(alergias.Select(x => new AlergiaIntoleranciaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> TraerAlergia(long alergiaId)
        {
            var alergia = await _alergiaIntoleranciaServicio.GetById(alergiaId);

            return Json(alergia, JsonRequestBehavior.AllowGet);
        }
    }
}
