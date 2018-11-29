using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.MacroNutriente;
using Servicio.Interface.Alimento;
using Servicio.Interface.MacroNutriente;

namespace NutricionWeb.Controllers.MacroNutriente
{
    public class MacroNutrienteController : Controller
    {
        private readonly IMacroNutrienteServicio _macroNutrienteServicio;
        private readonly IAlimentoServicio _alimentoServicio;

        public MacroNutrienteController(IMacroNutrienteServicio macroNutrienteServicio, IAlimentoServicio alimentoServicio)
        {
            _macroNutrienteServicio = macroNutrienteServicio;
            _alimentoServicio = alimentoServicio;
        }

        // GET: MacroNutriente
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Create(long alimentoId)
        {
            var alimento = await _alimentoServicio.GetById(alimentoId);

            return View(new MacroNutrienteABMViewModel()
            {
                AlimentoId = alimentoId,
                AlimentoStr = alimento.Descripcion
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MacroNutrienteABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var macroDto = CargarDatosMacro(vm);
                    macroDto.Codigo = await _macroNutrienteServicio.GetNextCode();
                    await _macroNutrienteServicio.Add(macroDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index","Alimento");
        }

        // GET: MacroNutriente/Edit/5
        public async Task<ActionResult> Edit(long? macroId)
        {
            if (macroId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var macro = await _macroNutrienteServicio.GetById(macroId.Value);

            return View(new MacroNutrienteABMViewModel()
            {
                Id = macro.Id,
                Codigo = macro.Codigo,
                AlimentoId = macro.AlimentoId,
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Eliminado = macro.Eliminado
            });
        }

        // POST: MacroNutriente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MacroNutrienteABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var macroDto = CargarDatosMacro(vm);
                    await _macroNutrienteServicio.Update(macroDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index","Alimento");
        }

        // GET: MacroNutriente/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var macro = await _macroNutrienteServicio.GetById(id.Value);

            return View(new MacroNutrienteViewModel()
            {
                Id = macro.Id,
                Codigo = macro.Codigo,
                AlimentoId = macro.AlimentoId,
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Eliminado = macro.Eliminado
            });
        }

        // POST: MacroNutriente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(MacroNutrienteViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _macroNutrienteServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: MacroNutriente/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var macro = await _macroNutrienteServicio.GetById(id.Value);

            return View(new MacroNutrienteViewModel()
            {
                Id = macro.Id,
                Codigo = macro.Codigo,
                AlimentoId = macro.AlimentoId,
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Eliminado = macro.Eliminado
            });
        }

        private MacroNutrienteDto CargarDatosMacro(MacroNutrienteABMViewModel vm)
        {
            return new MacroNutrienteDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                AlimentoId = vm.AlimentoId,
                AlimentoStr = vm.AlimentoStr,
                Energia = vm.Energia,
                Grasa = vm.Grasa,
                HidratosCarbono = vm.HidratosCarbono,
                Proteina = vm.Proteina,
                Eliminado = vm.Eliminado
            };
        }
    }
}
