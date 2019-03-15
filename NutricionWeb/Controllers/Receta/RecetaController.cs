using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.Receta;
using NutricionWeb.Models.RecetaDetalle;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.Receta;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Receta
{
    public class RecetaController : Controller
    {
        private readonly IRecetaServicio _recetaServicio;

        public RecetaController(IRecetaServicio recetaServicio)
        {
            _recetaServicio = recetaServicio;
        }

        // GET: Receta
        public async Task<ActionResult> Index(int? page,string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var recetas = await _recetaServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (recetas == null) return HttpNotFound();

            return View(recetas.Select(x => new RecetaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        

        // GET: Receta/Create
        public async Task<ActionResult> Create()
        {
            return View(new RecetaABMViewModel());
        }

        // POST: Receta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecetaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var recetaDto = CargarDatos(vm);
                    recetaDto.Codigo = await _recetaServicio.GetNextCode();

                    await _recetaServicio.Add(recetaDto);
                }
                
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Receta/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receta = await _recetaServicio.GetById(id.Value);

            return View(new RecetaABMViewModel()
            {
                Id = receta.Id,
                Codigo = receta.Codigo,
                Descripcion = receta.Descripcion,
                Eliminado = receta.Eliminado
            });
        }

        // POST: Receta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RecetaABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var recetaDto = CargarDatos(vm);

                    await _recetaServicio.Update(recetaDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Receta/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receta = await _recetaServicio.GetById(id.Value);

            return View(new RecetaViewModel()
            {
                Id = receta.Id,
                Codigo = receta.Codigo,
                Descripcion = receta.Descripcion,
                Eliminado = receta.Eliminado
            });
        }

        // POST: Receta/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(RecetaViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _recetaServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Receta/Details/5
        public async Task<ActionResult> Details(long? recetaId)
        {
            if (recetaId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var receta = await _recetaServicio.GetById(recetaId.Value);

            return View(new RecetaViewModel()
            {
                Id = receta.Id,
                Codigo = receta.Codigo,
                Descripcion = receta.Descripcion,
                Eliminado = receta.Eliminado,
                RecetasDetalles = receta.RecetasDetalles.Select(t => new RecetaDetalleViewModel()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    AlimentoId = t.AlimentoId,
                    AlimentoStr = t.AlimentoStr,
                    RecetaId = t.RecetaId,
                    RecetaStr = t.RecetaStr,
                    UnidadMedidaId = t.UnidadMedidaId,
                    UnidadMedidaStr = t.UnidadMedidaStr,
                    Cantidad = t.Cantidad,
                    Eliminado = t.Eliminado
                }).ToList()
            });
        }

        public async Task<ActionResult> BusquedaAlimentos()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BusquedaAlimentos(List<AlimentoViewModel> vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var alimentos = CargarAlimentos(vm);
                    await _recetaServicio.GetByFoods(alimentos);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("Index");
        }

        //===============================Hugo sonso
        private RecetaDto CargarDatos(RecetaABMViewModel vm)
        {
            return new RecetaDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }

        private List<AlimentoDto> CargarAlimentos(List<AlimentoViewModel> vm)
        {
            return vm.Select(x => new AlimentoDto()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToList();
        }
    }
}
