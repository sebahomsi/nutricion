using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Opcion;
using NutricionWeb.Models.OpcionDetalle;
using PagedList;
using Servicio.Interface.Opcion;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Opcion
{
    [Authorize(Roles = "Administrador")]
    public class OpcionController : Controller
    {
        private readonly IOpcionServicio _opcionServicio;

        public OpcionController(IOpcionServicio opcionServicio)
        {
            _opcionServicio = opcionServicio;
        }

        // GET: Opcion
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var opciones =
                await _opcionServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);


            return View(opciones.Select(x=> new OpcionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Opcion/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            var opcion = await _opcionServicio.GetById(id.Value);

            return View(new OpcionViewModel()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                Descripcion = opcion.Descripcion,
                Eliminado = opcion.Eliminado,
                OpcionDetalles = opcion.OpcionDetalles.Select(x => new OpcionDetalleViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    AlimentoId = x.AlimentoId,
                    AlimentoStr = x.AlimentoStr,
                    UnidadMedidaId = x.UnidadMedidaId,
                    UnidadMedidaStr = x.UnidadMedidaStr,
                    Cantidad = x.Cantidad,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.OpcionStr,
                    Eliminado = x.Eliminado
                }).ToList()
            });
        }

        // GET: Opcion/Create
        public async Task<ActionResult> Create()
        {
            return View(new OpcionABMViewModel());
        }

        // POST: Opcion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OpcionABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var opcionDto = CargarDatos(vm);
                    opcionDto.Codigo = await _opcionServicio.GetNextCode();

                    await _opcionServicio.Add(opcionDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: Opcion/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var opcion = await _opcionServicio.GetById(id.Value);

            return View(new OpcionABMViewModel()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                Descripcion = opcion.Descripcion,
                Eliminado = opcion.Eliminado
            });
        }

        // POST: Opcion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(OpcionABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var opcionDto = CargarDatos(vm);

                    await _opcionServicio.Update(opcionDto);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Opcion/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var opcion = await _opcionServicio.GetById(id.Value);

            return View(new OpcionViewModel()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                Descripcion = opcion.Descripcion,
                Eliminado = opcion.Eliminado
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(OpcionViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _opcionServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        //=======================Metodos con sindrome de Down

        private OpcionDto CargarDatos(OpcionABMViewModel vm)
        {
            return new OpcionDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
