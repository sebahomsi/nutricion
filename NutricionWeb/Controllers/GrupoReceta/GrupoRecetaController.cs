using NutricionWeb.Models.GrupoReceta;
using PagedList;
using Servicio.Interface.GrupoReceta;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.GrupoReceta
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class GrupoRecetaController : Controller
    {
        private readonly IGrupoRecetaServicio _grupoServicio;

        public GrupoRecetaController(IGrupoRecetaServicio grupoServicio)
        {
            _grupoServicio = grupoServicio;
        }

        // GET: Grupo
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;
            ViewBag.Eliminado = eliminado;

            var grupos = await _grupoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (grupos == null) return RedirectToAction("Error", "Home");

            return View(grupos.Select(x => new GrupoRecetaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Grupo/Create
        public async Task<ActionResult> Create()
        {
            return View(new GrupoRecetaViewModel());
        }

        // POST: Grupo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GrupoRecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var grupoDto = CargarDatos(vm);
                    grupoDto.Codigo = await _grupoServicio.GetNextCode();

                    await _grupoServicio.Add(grupoDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoRecetaViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        // POST: Grupo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GrupoRecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var grupoDto = CargarDatos(vm);

                    await _grupoServicio.Update(grupoDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoRecetaViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        // POST: Grupo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(GrupoRecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _grupoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Grupo/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var grupo = await _grupoServicio.GetById(id.Value);

            return View(new GrupoRecetaViewModel()
            {
                Id = grupo.Id,
                Codigo = grupo.Codigo,
                Descripcion = grupo.Descripcion,
                Eliminado = grupo.Eliminado
            });
        }

        //======================================================================//
        private GrupoRecetaDto CargarDatos(GrupoRecetaViewModel vm)
        {
            return new GrupoRecetaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
