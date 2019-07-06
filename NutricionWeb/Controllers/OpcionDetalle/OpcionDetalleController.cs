using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.OpcionDetalle;
using NutricionWeb.Models.UnidadMedida;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.Comida;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.OpcionDetalle;
using Servicio.Interface.UnidadMedida;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.OpcionDetalle
{
    [Authorize(Roles = "Administrador")]
    public class OpcionDetalleController : Controller
    {
        private readonly IOpcionDetalleServicio _opcionDetalleServicio;
        private readonly IAlimentoServicio _alimentoServicio;
        private readonly IUnidadMedidaServicio _unidadMedidaServicio;
        private readonly IOpcionServicio _opcionServicio;
        private readonly IComidaServicio _comidaServicio;
        private readonly IDiaServicio _diaServicio;

        public OpcionDetalleController(IOpcionDetalleServicio opcionDetalleServicio, IAlimentoServicio alimentoServicio, IUnidadMedidaServicio unidadMedidaServicio, IOpcionServicio opcionServicio, IComidaServicio comidaServicio, IDiaServicio diaServicio)
        {
            _opcionDetalleServicio = opcionDetalleServicio;
            _alimentoServicio = alimentoServicio;
            _unidadMedidaServicio = unidadMedidaServicio;
            _opcionServicio = opcionServicio;
            _comidaServicio = comidaServicio;
            _diaServicio = diaServicio;
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
            return RedirectToAction("Details", "Opcion", new {id = vm.OpcionId});

        }

        // GET: OpcionDetalle/Create
        public async Task<ActionResult> CreateRapido(long opcionId)
        {
            var opcion = await _opcionServicio.GetById(opcionId);
            var comida = await _comidaServicio.GetById(opcion.ComidaId);
            var dia = await _diaServicio.GetById(comida.DiaId);

            @ViewBag.Plan = dia.PlanAlimenticioId;

            return View(new OpcionDetalleABMViewModel()
            {
                OpcionId = opcionId
            });
        }

        // POST: OpcionDetalle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRapido(OpcionDetalleABMViewModel vm)
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
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            var opcion = await _opcionServicio.GetById(vm.OpcionId);
            var comida = await _comidaServicio.GetById(opcion.ComidaId);
            var dia = await _diaServicio.GetById(comida.DiaId);

            return RedirectToAction("ExportarPlan", "PlanAlimenticio", new { id = dia.PlanAlimenticioId });

        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var detalle = await _opcionDetalleServicio.GetById(id.Value);

            return View(new OpcionDetalleABMViewModel()
            {
                Id = detalle.Id,
                Codigo = detalle.Codigo,
                AlimentoId = detalle.AlimentoId,
                UnidadMedidaId = detalle.UnidadMedidaId,
                Cantidad = detalle.Cantidad,
                Eliminado = detalle.Eliminado,
                OpcionId = detalle.OpcionId,
                AlimentoStr = detalle.AlimentoStr,
                UnidadMedidaStr = detalle.UnidadMedidaStr
            });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OpcionDetalleABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var detalleDto = CargarDatos(vm);

                    await _opcionDetalleServicio.Update(detalleDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Details", "Opcion", new { id = vm.OpcionId });
        }

        // GET: OpcionDetalle/Delete/5
       
        public async Task<ActionResult> Delete(long detalleId)
        {
            await _opcionDetalleServicio.Delete(detalleId);
            var detalle = await _opcionDetalleServicio.GetById(detalleId);
            var opcionId = detalle.OpcionId;
            return RedirectToAction("Details", "Opcion", new {id = opcionId });
        }
        

        //=========================Metodos a prueba de hugos

        public async Task<ActionResult> BuscarAlimento(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            ViewBag.FilterValue = cadenaBuscar;

            var alimentos =
                await _alimentoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (alimentos == null) return RedirectToAction("Error", "Home");

            return PartialView(alimentos.Select(x => new AlimentoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                SubGrupoId = x.SubGrupoId,
                SubGrupoStr = x.SubGrupoStr,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }

        public async Task<ActionResult> BuscarUnidad(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;

            ViewBag.FilterValue = cadenaBuscar;

            var unidades =
                await _unidadMedidaServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (unidades == null) return RedirectToAction("Error", "Home");

            return PartialView(unidades.Select(x => new UnidadMedidaViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Abreviatura = x.Abreviatura,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
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
