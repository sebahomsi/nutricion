using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.MacroNutriente;
using Servicio.Interface.SubGrupo;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Alimento
{
    public class AlimentoController : Controller
    {
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly ISubGrupoServicio _subGrupoServicio;
        private readonly IMacroNutrienteServicio _macroNutrienteServicio;

        public AlimentoController(IAlimentoServicio alimentoServicio, ISubGrupoServicio subGrupoServicio, IMacroNutrienteServicio macroNutrienteServicio)
        {
            _alimentoServicio = alimentoServicio;
            _subGrupoServicio = subGrupoServicio;
            _macroNutrienteServicio = macroNutrienteServicio;
        }
        // GET: Alimento
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var alimentos =
                await _alimentoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (alimentos == null) return HttpNotFound();

            return View(alimentos.Select(x=> new AlimentoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                SubGrupoId = x.SubGrupoId,
                SubGrupoStr = x.SubGrupoStr,
                MacroNutrienteId = x.MacroNutrienteId,
                TieneMacroNutriente = x.TieneMacroNutriente,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Alimento/Create
        public async Task<ActionResult> Create()
        {
            return View(new AlimentoABMViewModel());
        }

        // POST: Alimento/Create
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

        // GET: Alimento/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var alimento = await _alimentoServicio.GetById(id.Value);

            return View(new AlimentoABMViewModel()
            {
                Id = alimento.Id,
                Codigo = alimento.Codigo,
                Descripcion = alimento.Descripcion,
                SubGrupoId = alimento.SubGrupoId,
                SubGrupoStr = alimento.SubGrupoStr,
                MacroNutrienteId = alimento.MacroNutrienteId,
                TieneMacroNutriente = alimento.TieneMacroNutriente,
                Eliminado = alimento.Eliminado
            });
        }

        // POST: Alimento/Edit/5
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Alimento/Delete/5
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

        // GET: Alimento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //=====================================================

        private AlimentoDto CargarDatos(AlimentoABMViewModel vm)
        {
            return new AlimentoDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                SubGrupoId = vm.SubGrupoId,
                SubGrupoStr = vm.SubGrupoStr,
                MacroNutrienteId = vm.MacroNutrienteId,
                TieneMacroNutriente = vm.TieneMacroNutriente,
                Eliminado = vm.Eliminado
            };
        }
    }
}
