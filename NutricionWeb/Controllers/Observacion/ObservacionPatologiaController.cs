using NutricionWeb.Models.ObservacionPatologia;
using NutricionWeb.Models.Patologia;
using PagedList;
using Servicio.Interface.ObservacionPatologia;
using Servicio.Interface.Patologia;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Observacion", new { id = vm.ObservacionId });
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
        public async Task<ActionResult> CreateParcial(long observacionId, long patologiaId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionPatologiaServicio.Add(observacionId, patologiaId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new { estado = false, mensaje = ex.Message });
            }
            return Json(new { estado = true });

        }


        // GET: ObservacionPatologia/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long patologiaId)
        {
            await _observacionPatologiaServicio.Delete(observacionId, patologiaId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }


        //===========================Metodos especiales
        public async Task<ActionResult> BuscarPatologia(int? page, string cadenaBuscar, long observacionId)
        {
            ViewBag.ObservacionId = observacionId;
            ViewBag.FilterValue = cadenaBuscar;

            var pageNumber = page ?? 1;
            var eliminado = false;
            var patologias =
                await _patologiaServicio.GetbyObservacionId(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, observacionId);

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

        public async Task<ActionResult> QuitarPatologia(long observacionId, long patologiaId)
        {
            try
            {
                await _observacionPatologiaServicio.Delete(observacionId, patologiaId);
            }
            catch (Exception e)
            {
                return Json(new { estado = false, mensaje = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { estado = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
