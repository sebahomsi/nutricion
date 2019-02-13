using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Opcion;
using NutricionWeb.Models.OpcionDetalle;
using PagedList;
using Servicio.Interface.Alimento;
using Servicio.Interface.Comida;
using Servicio.Interface.Opcion;
using Servicio.Interface.UnidadMedida;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Opcion
{
    [Authorize(Roles = "Administrador")]
    public class OpcionController : Controller
    {
        private readonly IOpcionServicio _opcionServicio;
        private readonly IComidaServicio _comidaServicio;

        public OpcionController(IOpcionServicio opcionServicio, IComidaServicio comidaServicio)
        {
            _opcionServicio = opcionServicio;
            _comidaServicio = comidaServicio;
        }

        // GET: Opcion
        public async Task<ActionResult> Index(long comidaId, int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var comida = await _comidaServicio.GetById(comidaId);
            var opciones = comida.Opciones;

            return View(opciones.Select(x=> new OpcionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                ComidaId = x.ComidaId,
                ComidaStr = x.ComidaStr,
                Descripcion = x.Descripcion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Opcion/Details/5
        public async Task<ActionResult> Details(long opcionId)
        {
            var opcion = await _opcionServicio.GetById(opcionId);

            return View(new OpcionViewModel()
            {
                Id = opcion.Id,
                Codigo = opcion.Codigo,
                ComidaId = opcion.ComidaId,
                ComidaStr = opcion.ComidaStr,
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
        public async Task<ActionResult> Create(long comidaId)
        {
            return View(new OpcionABMViewModel()
            {
                ComidaId = comidaId
            });
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
            return RedirectToAction("Details", "Comida", new {id = vm.ComidaId});

        }

        // GET: Opcion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Opcion/Edit/5
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

        // GET: Opcion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Opcion/Delete/5
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

        //=======================Metodos con sindrome de Down

        private OpcionDto CargarDatos(OpcionABMViewModel vm)
        {
            return new OpcionDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                ComidaId = vm.ComidaId,
                ComidaStr = vm.ComidaStr,
                Descripcion = vm.Descripcion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
