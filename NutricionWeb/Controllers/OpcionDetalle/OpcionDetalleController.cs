using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.OpcionDetalle;
using Servicio.Interface.OpcionDetalle;

namespace NutricionWeb.Controllers.OpcionDetalle
{
    public class OpcionDetalleController : Controller
    {
        private readonly IOpcionDetalleServicio _opcionDetalleServicio;

        public OpcionDetalleController(IOpcionDetalleServicio opcionDetalleServicio)
        {
            _opcionDetalleServicio = opcionDetalleServicio;
        }
        // GET: OpcionDetalle
        public ActionResult Index()
        {
            return View();
        }

        // GET: OpcionDetalle/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OpcionDetalle/Create
        public async Task<ActionResult> Create(long opcionId)
        {
            return View(new OpcionDetalleABMViewModel()
            {
                OpcionId = opcionId
            });
        }

        // POST: OpcionDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OpcionDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleDto = CargarDatos(vm);
                    detalleDto.Codigo = await _opcionDetalleServicio.GetNextCode();

                    await _opcionDetalleServicio.Add(detalleDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Opcion", new {opcionId = vm.OpcionId});

        }

        // GET: OpcionDetalle/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OpcionDetalle/Edit/5
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

        // GET: OpcionDetalle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OpcionDetalle/Delete/5
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
        //=========================Metodos a prueba de hugos

        private OpcionDetalleDto CargarDatos(OpcionDetalleABMViewModel vm)
        {
            return new OpcionDetalleDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                AlimentoId = vm.AlimentoId,
                AlimentoStr = vm.AlimentoStr,
                OpcionId = vm.OpcionId,
                OpcionStr = vm.OpcionStr,
                UnidadMedidaId = vm.UnidadMedidaId,
                UnidadMedidaStr = vm.UnidadMedidaStr,
                Cantidad = vm.Cantidad,
                Eliminado = vm.Eliminado
            };
        }
    }
}
