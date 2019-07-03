using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.ObservacionAlimento;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.ObservacionAlimento;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        // GET: ObservacionAlimento/Create
        public async Task<ActionResult> CreateParcial(long? observacionId, long? pacienteId)
        {
            ViewBag.Pacienteid = pacienteId ?? -1;
            TempData["Paciente"] = pacienteId ?? -1;
            return PartialView(new ObservacionAlimentoABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }

        // POST: ObservacionAlimento/Create
        [HttpPost]
        public async Task<ActionResult> CreateParcial(long observacionId, long alimentoId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionAlimentoServicio.Add(observacionId, alimentoId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new { estado = false, mensaje = ex.Message });
            }
            return Json(new { estado = true });
        }


        // GET: ObservacionAlimento/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long alimentoId)
        {
            await _observacionAlimentoServicio.Delete(observacionId, alimentoId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }

        //===========================Metodos especiales
        public async Task<ActionResult> BuscarAlimento(int? page, string cadenaBuscar, long observacionId)
        {
            ViewBag.ObservacionId = observacionId;
            ViewBag.FilterValue = cadenaBuscar;

            var pageNumber = page ?? 1;
            var eliminado = false;
            var alimentos =
                await _alimentoServicio.GetbyObservacionId(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, observacionId);

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

        public async Task<ActionResult> QuitarAlimento(long observacionId, long alimentoId)
        {
            try
            {
                await _observacionAlimentoServicio.Delete(observacionId, alimentoId);
            }
            catch (Exception e)
            {
                return Json(new { estado = false, mensaje = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { estado = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
