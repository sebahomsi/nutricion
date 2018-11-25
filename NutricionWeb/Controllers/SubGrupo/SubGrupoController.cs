using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Grupo;
using NutricionWeb.Models.SubGrupo;
using PagedList;
using Servicio.Interface.Grupo;
using Servicio.Interface.SubGrupo;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.SubGrupo
{
    public class SubGrupoController : Controller
    {
        private readonly ISubGrupoServicio _subGrupoServicio;
        private readonly IGrupoServicio _grupoServicio;

        public SubGrupoController(ISubGrupoServicio subGrupoServicio, IGrupoServicio grupoServicio)
        {
            _subGrupoServicio = subGrupoServicio;
            _grupoServicio = grupoServicio;
        }
        // GET: SubGrupo
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var subGrupos =
                await _subGrupoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (subGrupos == null) return HttpNotFound();

            return View(subGrupos.Select(x=> new SubGrupoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                GrupoId = x.GrupoId,
                GrupoStr = x.GrupoStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber,CantidadFilasPorPaginas));
        }

        // GET: SubGrupo/Create
        public async Task<ActionResult> Create()
        {
            return View(new SubGrupoABMViewModel());
        }

        // POST: SubGrupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubGrupoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subGrupoDto = CargarDatos(vm);
                    subGrupoDto.Codigo = await _subGrupoServicio.GetNextCode();

                    await _subGrupoServicio.Add(subGrupoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: SubGrupo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SubGrupo/Edit/5
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

        // GET: SubGrupo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SubGrupo/Delete/5
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

        // GET: SubGrupo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        //===========================================================//

        public async Task<ActionResult> BuscarGrupo(string cadenaBuscar)
        {
            var grupos = await _grupoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            ViewBag.Registros = grupos.Count;

            return PartialView(grupos.Take(5).Select(x => new GrupoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToList());
        }

        public async Task<ActionResult> TraerGrupo(long grupoId)
        {
            var grupo = await _grupoServicio.GetById(grupoId);

            return Json(grupo,JsonRequestBehavior.AllowGet);
        }
        private SubGrupoDto CargarDatos(SubGrupoABMViewModel vm)
        {
            return new SubGrupoDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                GrupoId = vm.GrupoId,
                GrupoStr = vm.GrupoStr,
                Eliminado = vm.Eliminado
            };
        }
    }
}
