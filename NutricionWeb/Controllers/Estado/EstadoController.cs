using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using NutricionWeb.Models.Estado;
using PagedList;
using Servicio.Interface.Estado;
using Servicio.Interface.Estado.Dto;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Estado
{
    public class EstadoController : Controller
    {
        private readonly IEstadoServicio _estadoServicio;

        public EstadoController(IEstadoServicio estadoServicio)
        {
            _estadoServicio = estadoServicio;
        }

        // GET: Estado
        public async Task<ActionResult> Index(int? page,string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var datos = await _estadoServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar)? cadenaBuscar:string.Empty);

            var estados = Mapper.Map<IEnumerable<EstadoViewModel>>(datos);

            return View(estados.ToPagedList(pageNumber,CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> Create()
        {
            return await Task.Run(() => View(new EstadoViewModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EstadoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoDto = CargarDatos(vm);

                    estadoDto.Color = "#" + vm.Color;

                    estadoDto.Codigo = await _estadoServicio.GetNextCode();

                    await _estadoServicio.Add(estadoDto);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _estadoServicio.GetById(id.Value);

            if (dato == null) return HttpNotFound();

            var estado = Mapper.Map<EstadoViewModel>(dato);

            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EstadoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var estadoDto = CargarDatos(vm);
                    estadoDto.Color = "#" + vm.Color;
                    await _estadoServicio.Update(estadoDto);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _estadoServicio.GetById(id.Value);

            if (dato == null) return RedirectToAction("Error", "Home");

            var estado = Mapper.Map<EstadoViewModel>(dato);

            return View(estado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EstadoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _estadoServicio.Delete(vm.Id);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }



        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _estadoServicio.GetById(id.Value);

            if (dato == null) return HttpNotFound();

            var estado = Mapper.Map<EstadoViewModel>(dato);

            return View(estado);
        }


        private static EstadoDto CargarDatos(EstadoViewModel vm)
        {
            return Mapper.Map<EstadoDto>(vm);
        }
    }
}