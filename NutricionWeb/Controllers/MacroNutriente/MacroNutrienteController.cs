using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NutricionWeb.Models.MacroNutriente;
using Servicio.Interface.Alimento;
using Servicio.Interface.MacroNutriente;

namespace NutricionWeb.Controllers.MacroNutriente
{
    [Authorize(Roles = "Administrador")]
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
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var macro = await _macroNutrienteServicio.GetById(id.Value);

            return View(new MacroNutrienteABMViewModel()
            {
                Id = macro.Id,
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Calorias = macro.Calorias,
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
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Calorias = macro.Calorias,
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
                AlimentoStr = macro.AlimentoStr,
                HidratosCarbono = macro.HidratosCarbono,
                Energia = macro.Energia,
                Grasa = macro.Grasa,
                Proteina = macro.Proteina,
                Calorias = macro.Calorias,
                Eliminado = macro.Eliminado
            });
        }

        //===========================Hugo masticador de penes

        public ActionResult CalcularCalorias(int prote, int hc, int grasa)
        {
            //if (prote == null || hc == null || grasa == null)
            //    return Json(total, JsonRequestBehavior.AllowGet);

            var total = (prote * 4) + (hc*4) + (grasa * 9);

            return Json(total, JsonRequestBehavior.AllowGet);

        }

        private MacroNutrienteDto CargarDatosMacro(MacroNutrienteABMViewModel vm)
        {
            return new MacroNutrienteDto()
            {
                Id = vm.Id,
                AlimentoStr = vm.AlimentoStr,
                Energia = vm.Energia,
                Grasa = vm.Grasa,
                HidratosCarbono = vm.HidratosCarbono,
                Proteina = vm.Proteina,
                Calorias = vm.Calorias,
                Eliminado = vm.Eliminado
            };
        }
    }
}
