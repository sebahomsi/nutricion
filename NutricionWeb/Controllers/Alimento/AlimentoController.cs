using NutricionWeb.Helpers.SubGrupo;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.MacroNutriente;
using NutricionWeb.Models.SubGrupo;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.MacroNutriente;
using Servicio.Interface.SubGrupo;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Alimento
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class AlimentoController : Controller
    {
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly ISubGrupoServicio _subGrupoServicio;
        private readonly IMacroNutrienteServicio _macroNutrienteServicio;
        private readonly IComboBoxSubGrupo _comboBoxSubGrupo;

        public AlimentoController(IAlimentoServicio alimentoServicio, ISubGrupoServicio subGrupoServicio, IMacroNutrienteServicio macroNutrienteServicio, IComboBoxSubGrupo comboBoxSubGrupo)
        {
            _alimentoServicio = alimentoServicio;
            _subGrupoServicio = subGrupoServicio;
            _macroNutrienteServicio = macroNutrienteServicio;
            _comboBoxSubGrupo = comboBoxSubGrupo;
        }

        
        // GET: Alimento
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar,bool eliminado = false)
        {
            var pageNumber = page ?? 1;
            ViewBag.FilterValue = cadenaBuscar;
            ViewBag.Eliminado = eliminado;

            var alimentos =
                await _alimentoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (alimentos == null) return HttpNotFound();

            return View(alimentos.Select(x=> new AlimentoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                SubGrupoId = x.SubGrupoId,
                SubGrupoStr = x.SubGrupoStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Alimento/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Create()
        {
            return View(new AlimentoABMViewModel());
        }

        // POST: Alimento/Create
        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlimentoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var alimentoDto = CargarDatos(vm);
                    alimentoDto.Codigo = await _alimentoServicio.GetNextCode();

                    await _alimentoServicio.Add(alimentoDto);
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
            return PartialView(new AlimentoABMViewModel()
            {
                SubGrupos = await _comboBoxSubGrupo.Poblar()
            });
        }

        // POST: Alimento/Create
        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(AlimentoABMViewModel vm)
        {
            
            try
            {
               
                    var alimentoDto = CargarDatos(vm);
                    alimentoDto.Codigo = await _alimentoServicio.GetNextCode();

                    await _alimentoServicio.Add(alimentoDto);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                vm.SubGrupos = await _comboBoxSubGrupo.Poblar();
                return Json(new { estado = false, vista = RenderRazorViewToString("~/View/Alimento/CreateParcial.cshtml", vm) });
            }
            return Json(new { estado = true });
        }

        // GET: Alimento/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var alimento = await _alimentoServicio.GetById(id.Value);

            return View(new AlimentoABMViewModel()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupoStr,
                Eliminado = alimento.Eliminado,
                MacroNutriente = new MacroNutrienteViewModel()
                {
                    Id = alimento.MacroNutriente.Id,
                    HidratosCarbono = alimento.MacroNutriente.HidratosCarbono,
                    Grasa = alimento.MacroNutriente.Grasa,
                    Proteina = alimento.MacroNutriente.Proteina,
                    Energia = alimento.MacroNutriente.Energia,
                    Calorias = alimento.MacroNutriente.Calorias
                }
            });
        }

        // POST: Alimento/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AlimentoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var alimentoDto = CargarDatos(vm);
                    await _alimentoServicio.Update(alimentoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: Alimento/Delete/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var alimento = await _alimentoServicio.GetById(id.Value);

            return View(new AlimentoViewModel()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupoStr,
                Eliminado = alimento.Eliminado,
                MacroNutriente = new MacroNutrienteViewModel()
                {
                    Id = alimento.MacroNutriente.Id,
                    HidratosCarbono = alimento.MacroNutriente.HidratosCarbono,
                    Grasa = alimento.MacroNutriente.Grasa,
                    Proteina = alimento.MacroNutriente.Proteina,
                    Energia = alimento.MacroNutriente.Energia,
                    Calorias = alimento.MacroNutriente.Calorias
                }
            });
        }

        // POST: Alimento/Delete/5
        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(AlimentoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _alimentoServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Alimento/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var alimento = await _alimentoServicio.GetById(id.Value);

            return View(new AlimentoViewModel()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupoStr,
                Eliminado = alimento.Eliminado,
                MacroNutriente = new MacroNutrienteViewModel()
                {
                    Id = alimento.MacroNutriente.Id,
                    HidratosCarbono = alimento.MacroNutriente.HidratosCarbono,
                    Grasa = alimento.MacroNutriente.Grasa,
                    Proteina = alimento.MacroNutriente.Proteina,
                    Energia = alimento.MacroNutriente.Energia,
                    Calorias = alimento.MacroNutriente.Calorias
                }
            });
        }



        //=====================================================
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> TraerSubGrupo(long subGrupoId)
        {
            var subGrupo = await _subGrupoServicio.GetById(subGrupoId);

            return Json(subGrupo, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> BuscarSubGrupo(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            ViewBag.FilterValue = cadenaBuscar;

            var subGrupos =
                await _subGrupoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return PartialView(subGrupos.Select(x => new SubGrupoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                GrupoId = x.GrupoId,
                GrupoStr = x.GrupoStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }
        [Authorize(Roles = "Administrador, Empleado")]
        public ActionResult CalcularCalorias(decimal prote, decimal hc, decimal grasa)
        {
            //if (prote == null || hc == null || grasa == null)
            //    return Json(total, JsonRequestBehavior.AllowGet);

            var total = (prote * 4) + (hc * 4) + (grasa * 9);

            return Json(total, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Administrador, Empleado")]
        private AlimentoDto CargarDatos(AlimentoABMViewModel vm)
        {
            return new AlimentoDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                SubGrupoId = vm.SubGrupoId,
                SubGrupoStr = vm.SubGrupoStr,
                Eliminado = vm.Eliminado,
                MacroNutriente = new MacroNutrienteDto()
                {
                    Id = vm.MacroNutriente.Id,
                    Energia = vm.MacroNutriente.Energia,
                    Grasa = vm.MacroNutriente.Grasa,
                    HidratosCarbono = vm.MacroNutriente.HidratosCarbono,
                    Proteina = vm.MacroNutriente.Proteina,
                    Calorias = vm.MacroNutriente.Calorias,
                    Eliminado = vm.MacroNutriente.Eliminado
                }
            };
        }

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


    }
}
