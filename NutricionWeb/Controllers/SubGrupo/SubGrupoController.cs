using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    [Authorize(Roles = "Administrador, Empleado")]
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
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var subGrupos =
                await _subGrupoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (subGrupos == null) return RedirectToAction("Error", "Home");

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
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var subGrupo = await _subGrupoServicio.GetById(id.Value);

            return View(new SubGrupoABMViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoId = subGrupo.GrupoId,
                GrupoStr = subGrupo.GrupoStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        // POST: SubGrupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubGrupoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subGrupoDto = CargarDatos(vm);
                    await _subGrupoServicio.Update(subGrupoDto);
                }

            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: SubGrupo/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var subGrupo = await _subGrupoServicio.GetById(id.Value);

            return View(new SubGrupoViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoId = subGrupo.GrupoId,
                GrupoStr = subGrupo.GrupoStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        // POST: SubGrupo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SubGrupoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _subGrupoServicio.Delete(vm.Id);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: SubGrupo/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var subGrupo = await _subGrupoServicio.GetById(id.Value);

            return View(new SubGrupoViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoId = subGrupo.GrupoId,
                GrupoStr = subGrupo.GrupoStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        //===========================================================//
        public async Task<ActionResult> BuscarGrupo(int? page,string cadenaBuscar)
        {
            ViewBag.FilterValue = cadenaBuscar;

            var eliminado = false;
            var pageNumber = page ?? 1;
            var grupos = await _grupoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            
            ViewBag.Registros = grupos.Count;

            return PartialView(grupos.Select(x => new GrupoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
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
