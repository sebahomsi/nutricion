using NutricionWeb.Models.GrupoReceta;
using NutricionWeb.Models.SubGrupoReceta;
using PagedList;
using Servicio.Interface.GrupoReceta;
using Servicio.Interface.SubGrupoReceta;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.SubGrupoReceta
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class SubGrupoRecetaController : Controller
    {
        private readonly ISubGrupoRecetaServicio _subGrupoServicio;
        private readonly IGrupoRecetaServicio _grupoServicio;

        public SubGrupoRecetaController(ISubGrupoRecetaServicio subGrupoServicio, IGrupoRecetaServicio grupoServicio)
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
                await _subGrupoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (subGrupos == null) return RedirectToAction("Error", "Home");

            return View(subGrupos.Select(x => new SubGrupoRecetaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                GrupoRecetaId = x.GrupoRecetaId,
                GrupoRecetaStr = x.GrupoRecetaStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: SubGrupo/Create
        public async Task<ActionResult> Create()
        {
            return View(new SubGrupoRecetaViewModel());
        }

        // POST: SubGrupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubGrupoRecetaViewModel vm)
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Se produjo un error al cargar los datos, revise los campos.");
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: SubGrupo/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var subGrupo = await _subGrupoServicio.GetById(id.Value);

            return View(new SubGrupoRecetaViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoRecetaId = subGrupo.GrupoRecetaId,
                GrupoRecetaStr = subGrupo.GrupoRecetaStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        // POST: SubGrupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubGrupoRecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var subGrupoDto = CargarDatos(vm);
                    await _subGrupoServicio.Update(subGrupoDto);
                }

            }
            catch (Exception ex)
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

            return View(new SubGrupoRecetaViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoRecetaId = subGrupo.GrupoRecetaId,
                GrupoRecetaStr = subGrupo.GrupoRecetaStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        // POST: SubGrupo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(SubGrupoRecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _subGrupoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
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

            return View(new SubGrupoRecetaViewModel()
            {
                Id = subGrupo.Id,
                Codigo = subGrupo.Codigo,
                GrupoRecetaId = subGrupo.GrupoRecetaId,
                GrupoRecetaStr = subGrupo.GrupoRecetaStr,
                Descripcion = subGrupo.Descripcion,
                Eliminado = subGrupo.Eliminado
            });
        }

        //===========================================================//
        public async Task<ActionResult> BuscarGrupo(int? page, string cadenaBuscar)
        {
            var eliminado = false;
            var pageNumber = page ?? 1;
            var grupos = await _grupoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            ViewBag.Registros = grupos.Count;

            return PartialView(grupos.Select(x => new GrupoRecetaViewModel()
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

            return Json(grupo, JsonRequestBehavior.AllowGet);
        }

        private SubGrupoRecetaDto CargarDatos(SubGrupoRecetaViewModel vm)
        {
            return new SubGrupoRecetaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                GrupoRecetaId = vm.GrupoRecetaId,
                GrupoRecetaStr = vm.GrupoRecetaStr,
                Eliminado = vm.Eliminado
            };
        }
    }
}
