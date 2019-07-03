using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.ObservacionAlergiaIntolerancia;
using PagedList;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.ObservacionAlergiaIntolerancia;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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


        public async Task<ActionResult> CreateParcial(long? observacionId, long? pacienteId)
        {
            ViewBag.PacienteId = pacienteId.Value;
            TempData["Paciente"] = pacienteId.Value;
            return PartialView(new ObservacionAlergiaIntoleranciaABMViewModel()
            {
                ObservacionId = observacionId.Value
            });
        }


        [HttpPost]
        public async Task<ActionResult> CreateParcial(long observacionId, long alergiaId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _observacionAlergiaIntoleranciaServicio.Add(observacionId, alergiaId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new { estado = false, mensaje = ex.Message });
            }
            return Json(new { estado = true });
        }


        // GET: ObservacionAlergiaIntolerancia/Delete/5
        public async Task<ActionResult> Delete(long observacionId, long alergiaId)
        {
            await _observacionAlergiaIntoleranciaServicio.Delete(observacionId, alergiaId);
            return RedirectToAction("Details", "Observacion", new { id = observacionId });
        }

 

        //===========================Metodos especiales
        public async Task<ActionResult> BuscarAlergia(int? page, string cadenaBuscar, long observacionId)
        {

            ViewBag.ObservacionId = observacionId;
            ViewBag.FilterValue = cadenaBuscar;

            var pageNumber = page ?? 1;
            var eliminado = false;
            var alergias =
                await _alergiaIntoleranciaServicio.GetbyObservacionId(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty, observacionId);

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

        public async Task<ActionResult> QuitarAlergia(long observacionId, long alergiaId)
        {
            try
            {
                await _observacionAlergiaIntoleranciaServicio.Delete(observacionId, alergiaId);
            }
            catch (Exception e)
            {
                return Json(new { estado = false, mensaje = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { estado = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
