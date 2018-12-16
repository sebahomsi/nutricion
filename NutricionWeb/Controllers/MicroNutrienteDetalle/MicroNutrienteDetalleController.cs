using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.MicroNutriente;
using NutricionWeb.Models.MicroNutrienteDetalle;
using NutricionWeb.Models.UnidadMedida;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.MicroNutriente;
using Servicio.Interface.MicroNutrienteDetalle;
using Servicio.Interface.UnidadMedida;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.MicroNutrienteDetalle
{
    public class MicroNutrienteDetalleController : Controller
    {
        private readonly IMicroNutrienteDetalleServicio _microNutrienteDetalleServicio;
        private readonly IMicroNutrienteServicio _microNutrienteServicio;
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IAlimentoServicio _alimentoServicio;

        public MicroNutrienteDetalleController(IMicroNutrienteDetalleServicio microNutrienteDetalleServicio, IMicroNutrienteServicio microNutrienteServicio, IUnidadMedidaServicio unidadMedidaServicio, IAlimentoServicio alimentoServicio)
        {
            _microNutrienteDetalleServicio = microNutrienteDetalleServicio;
            _microNutrienteServicio = microNutrienteServicio;
            _unidadMedidaServicio = unidadMedidaServicio;
            _alimentoServicio = alimentoServicio;
        }

        // GET: MicroNutrienteDetalle
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var detalles =
                await _microNutrienteDetalleServicio.Get(!string.IsNullOrEmpty(cadenaBuscar)
                    ? cadenaBuscar
                    : string.Empty);

            if (detalles == null) return HttpNotFound();

            return View(detalles.Select(x=> new MicroNutrienteDetalleViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                AlimentoId = x.AlimentoId,
                AlimentoStr = x.AlimentoStr,
                MicroNutrienteId = x.MicroNutrienteId,
                MicroNutrienteStr = x.MicroNutrienteStr,
                UnidadMedidaId = x.UnidadMedidaId,
                UnidadMedidaStr = x.UnidadMedidaStr,
                Cantidad = x.Cantidad
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }
        

        // GET: MicroNutrienteDetalle/Create
        public async Task<ActionResult> Create()
        {
            return View(new MicroNutrienteDetalleABMViewModel());
        }

        // POST: MicroNutrienteDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MicroNutrienteDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleDto = CargarDatos(vm);

                    detalleDto.Codigo = await _microNutrienteDetalleServicio.GetNextCode();

                    await _microNutrienteDetalleServicio.Add(detalleDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");

        }

        // GET: MicroNutrienteDetalle/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if(id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detalle = await _microNutrienteDetalleServicio.GetById(id.Value);

            return View(new MicroNutrienteDetalleABMViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.AlimentoStr,
                MicroNutrienteId = detalle.MicroNutrienteId,
                MicroNutrienteStr = detalle.MicroNutrienteStr,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedidaStr,
                Cantidad = detalle.Cantidad
            });
        }

        // POST: MicroNutrienteDetalle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MicroNutrienteDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleDto = CargarDatos(vm);

                    await _microNutrienteDetalleServicio.Update(detalleDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: MicroNutrienteDetalle/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detalle = await _microNutrienteDetalleServicio.GetById(id.Value);

            return View(new MicroNutrienteDetalleViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.AlimentoStr,
                MicroNutrienteId = detalle.MicroNutrienteId,
                MicroNutrienteStr = detalle.MicroNutrienteStr,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedidaStr,
                Cantidad = detalle.Cantidad
            });
        }

        // POST: MicroNutrienteDetalle/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(MicroNutrienteDetalleViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _microNutrienteDetalleServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: MicroNutrienteDetalle/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detalle = await _microNutrienteDetalleServicio.GetById(id.Value);

            return View(new MicroNutrienteDetalleViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                AlimentoStr = detalle.AlimentoStr,
                MicroNutrienteId = detalle.MicroNutrienteId,
                MicroNutrienteStr = detalle.MicroNutrienteStr,
                UnidadMedidaId = detalle.UnidadMedidaId,
                UnidadMedidaStr = detalle.UnidadMedidaStr,
                Cantidad = detalle.Cantidad
            });
        }

        //==========================================Métodos Privados

        public async Task<ActionResult> BuscarAlimento(int? page,string cadenaBuscar)
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

        public async Task<ActionResult> BuscarMicroNutriente(int? page,string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var micros =
                await _microNutrienteServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (micros == null) return HttpNotFound();

            return PartialView(micros.Select(x => new MicroNutrienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
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

        public async Task<ActionResult> TraerMicroNutriente(long microId)
        {
            var micro = await _microNutrienteServicio.GetById(microId);

            return Json(micro, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TraerUnidad(long unidadId)
        {
            var unidad = await _unidadMedidaServicio.GetById(unidadId);

            return Json(unidad, JsonRequestBehavior.AllowGet);
        }


        private MicroNutrienteDetalleDto CargarDatos(MicroNutrienteDetalleABMViewModel vm)
        {
            return new MicroNutrienteDetalleDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                AlimentoId = vm.AlimentoId,
                AlimentoStr = vm.AlimentoStr,
                MicroNutrienteId = vm.MicroNutrienteId,
                MicroNutrienteStr = vm.MicroNutrienteStr,
                UnidadMedidaId = vm.UnidadMedidaId,
                UnidadMedidaStr = vm.UnidadMedidaStr,
                Cantidad = vm.Cantidad
            };
        }
    }
}
