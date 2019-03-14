using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.RecetaDetalle;
using Servicio.Interface.RecetaDetalle;

namespace NutricionWeb.Controllers.RecetaDetalle
{
    public class RecetaDetalleController : Controller
    {
        private readonly IRecetaDetalleServicio _recetaDetalleServicio;

        public RecetaDetalleController(IRecetaDetalleServicio recetaDetalleServicio)
        {
            _recetaDetalleServicio = recetaDetalleServicio;
        }

        // GET: RecetaDetalle
        public async Task<ActionResult> Index()
        {
            return View();
        }

        // GET: RecetaDetalle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RecetaDetalle/Create
        public async Task<ActionResult> Create(long recetaId)
        {
            return View(new RecetaDetalleABMViewModel()
            {
                RecetaId = recetaId
            });
        }

        // POST: RecetaDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RecetaDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleDto = CargarDatos(vm);
                    detalleDto.Codigo = await _recetaDetalleServicio.GetNextCode();

                    await _recetaDetalleServicio.Add(detalleDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Receta", new { recetaId = vm.RecetaId });
        }

        // GET: RecetaDetalle/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: RecetaDetalle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
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

        // GET: RecetaDetalle/Delete/5
        public async Task<ActionResult> Delete(long detalleId)
        {
            await _recetaDetalleServicio.Delete(detalleId); 
            var detalle = await _recetaDetalleServicio.GetById(detalleId);
            var id = detalle.RecetaId;
            return RedirectToAction("Details", "Receta", new { recetaId = id });
        }

        

        private RecetaDetalleDto CargarDatos(RecetaDetalleABMViewModel vm)
        {
            return new RecetaDetalleDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                AlimentoId = vm.AlimentoId,
                AlimentoStr = vm.AlimentoStr,
                RecetaId = vm.RecetaId,
                RecetaStr = vm.RecetaStr,
                UnidadMedidaId = vm.UnidadMedidaId,
                UnidadMedidaStr = vm.UnidadMedidaStr,
                Cantidad = vm.Cantidad,
                Eliminado = vm.Eliminado
            };
        }
    }
}
