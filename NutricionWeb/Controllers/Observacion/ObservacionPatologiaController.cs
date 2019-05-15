using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.ObservacionPatologia;
using NutricionWeb.Models.Patologia;
using PagedList;
using Servicio.Interface.ObservacionPatologia;
using Servicio.Interface.Patologia;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Observacion
{
    [Authorize(Roles = "Administrador")]
    public class ObservacionPatologiaController : Controller
    {
        private readonly IObservacionPatologiaServicio _observacionPatologiaServicio;
        private readonly IPatologiaServicio _patologiaServicio;

        public ObservacionPatologiaController(IObservacionPatologiaServicio observacionPatologiaServicio, IPatologiaServicio patologiaServicio)
        {
            _observacionPatologiaServicio = observacionPatologiaServicio;
            _patologiaServicio = patologiaServicio;
        }

        // GET: ObservacionPatologia
        public ActionResult Index()
        {
            return View();
        }

        // GET: ObservacionPatologia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ObservacionPatologia/Create
        public async Task<ActionResult> Create(long? observacionId)
        {
            return View(new ObservacionPatologiaABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }

        // POST: ObservacionPatologia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObservacionPatologiaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionPatologiaServicio.Add(vm.ObservacionId, vm.PatologiaId);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Observacion", new {id = vm.ObservacionId});
        }

        public async Task<ActionResult> CreateParcial(long? observacionId, long? pacienteId)
        {
            ViewBag.Pacienteid = pacienteId.Value;
            TempData["Paciente"] = pacienteId.Value;
            return PartialView(new ObservacionPatologiaABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }

        // POST: ObservacionPatologia/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(ObservacionPatologiaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionPatologiaServicio.Add(vm.ObservacionId, vm.PatologiaId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("ObservacionesParcial", "Paciente", new { id = TempData["Paciente"] });
        }

        // GET: ObservacionPatologia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ObservacionPatologia/Edit/5
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

        // GET: ObservacionPatologia/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long patologiaId)
        {
            await _observacionPatologiaServicio.Delete(observacionId, patologiaId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }

        // POST: ObservacionPatologia/Delete/5
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
        public async Task<ActionResult> BuscarPatologia(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;
            var patologias =
                await _patologiaServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(patologias.Select(x => new PatologiaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> TraerPatologia(long patologiaId)
        {
            var patologia = await _patologiaServicio.GetById(patologiaId);

            return Json(patologia, JsonRequestBehavior.AllowGet);
        }
    }
}
