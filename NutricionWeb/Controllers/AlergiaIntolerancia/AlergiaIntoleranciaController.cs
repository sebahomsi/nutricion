using NutricionWeb.Models.AlergiaIntolerancia;
using PagedList;
using Servicio.Interface.AlergiaIntolerancia;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
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

        [Authorize(Roles = "Administrador, Empleado")]
        // GET: AlergiaIntolerancia
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;
            ViewBag.FilterValue = cadenaBuscar;
            var alergiasIntolerancias =
                await _alergiaIntoleranciaServicio.Get(eliminado,
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

        [Authorize(Roles = "Administrador, Empleado")]

        // GET: AlergiaIntolerancia/Create
        public async Task<ActionResult> Create()
        {
            return View(new AlergiaIntoleranciaABMViewModel());
        }
        [Authorize(Roles = "Administrador, Empleado")]
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

        public async Task<ActionResult> CreateParcial(long? observacionId)
        {

            ViewBag.ObservacionId = observacionId ?? -1;
            return PartialView(new AlergiaIntoleranciaABMViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(AlergiaIntoleranciaABMViewModel vm)
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return Json(new { estado = false, vista = RenderRazorViewToString("~/View/AlergiaIntolerancia/CreateParcial.cshtml", vm) });
            }
            return Json(new { estado = true });
        }

        [Authorize(Roles = "Administrador, Empleado")]
        // GET: AlergiaIntolerancia/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var alergia = await _alergiaIntoleranciaServicio.GetById(id.Value);

            return View(new AlergiaIntoleranciaABMViewModel()
            {
                Id = alergia.Id,
                Codigo = alergia.Codigo,
                Descripcion = alergia.Descripcion,
                Eliminado = alergia.Eliminado
            });
        }
        [Authorize(Roles = "Administrador, Empleado")]
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
            if (id == null) return RedirectToAction("Error", "Home");

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
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [Authorize(Roles = "Administrador, Empleado")]
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
