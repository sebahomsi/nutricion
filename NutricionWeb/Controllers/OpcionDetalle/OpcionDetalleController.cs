using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.OpcionDetalle;
using NutricionWeb.Models.UnidadMedida;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.OpcionDetalle;
using Servicio.Interface.UnidadMedida;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.OpcionDetalle
{
    public class OpcionDetalleController : Controller
    {
        private readonly IOpcionDetalleServicio _opcionDetalleServicio;
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;

        public OpcionDetalleController(IOpcionDetalleServicio opcionDetalleServicio, IAlimentoServicio alimentoServicio, IUnidadMedidaServicio unidadMedidaServicio)
        {
            _opcionDetalleServicio = opcionDetalleServicio;
            _alimentoServicio = alimentoServicio;
            _unidadMedidaServicio = unidadMedidaServicio;
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

        public async Task<ActionResult> BuscarAlimento(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var alimentos =
                await _alimentoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (alimentos == null) return HttpNotFound();

            return PartialView(alimentos.Select(x => new AlimentoViewModel()
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

        public async Task<ActionResult> BuscarUnidad(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var unidades =
                await _unidadMedidaServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (unidades == null) return HttpNotFound();

            return PartialView(unidades.Select(x => new UnidadMedidaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Abreviatura = x.Abreviatura,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> TraerAlimento(long alimentoId)
        {
            var alimento = await _alimentoServicio.GetById(alimentoId);

            return Json(alimento, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TraerUnidad(long unidadId)
        {
            var unidad = await _unidadMedidaServicio.GetById(unidadId);

            return Json(unidad, JsonRequestBehavior.AllowGet);
        }

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
