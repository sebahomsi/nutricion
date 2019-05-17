using NutricionWeb.Models.Establecimiento;
using Servicio.Interface.Establecimiento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using PagedList;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Establecimiento
{
    public class EstablecimientoController : Controller
    {

        private readonly IEstablecimientoServicio _establecimientoServicio;

        public EstablecimientoController(IEstablecimientoServicio establecimientoServicio)
        {
            _establecimientoServicio = establecimientoServicio;
        }

        // GET: Establecimiento
        public async Task<ActionResult> Index(int? page,string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var establecimientos = await _establecimientoServicio.Get();

            var vm = Mapper.Map<IEnumerable<EstablecimientoViewModel>>(establecimientos);

            return View(vm.ToPagedList(pageNumber,CantidadFilasPorPaginas));
        }

        // GET: Establecimiento/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var establecimiento = await _establecimientoServicio.GetById(id.Value);

            if (establecimiento == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(new EstablecimientoViewModel()
            {
                Id = establecimiento.Id,
                Nombre = establecimiento.Nombre,
                Direccion = establecimiento.Direccion,
                Profesional = establecimiento.Profesional,
                Email = establecimiento.Email,
                Facebook = establecimiento.Facebook,
                Horario = establecimiento.Horario,
                Instagram = establecimiento.Instagram,
                Telefono = establecimiento.Telefono,
                Twitter = establecimiento.Twitter
            });
        }

        public  ActionResult Create()
        {
            return View(new EstablecimientoViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EstablecimientoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var establecimientoDto = CargarDatos(vm);
                    await _establecimientoServicio.Add(establecimientoDto);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,e.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }



        // GET: Establecimiento/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var establecimiento = await _establecimientoServicio.GetById(id.Value);

            return View(new EstablecimientoViewModel()
            {
                Id = establecimiento.Id,
                Nombre = establecimiento.Nombre,
                Direccion = establecimiento.Direccion,
                Profesional = establecimiento.Profesional,
                Email = establecimiento.Email,
                Facebook = establecimiento.Facebook,
                Horario = establecimiento.Horario,
                Instagram = establecimiento.Instagram,
                Telefono = establecimiento.Telefono,
                Twitter = establecimiento.Twitter
            });
        }

        // POST: Establecimiento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EstablecimientoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);

                    await _establecimientoServicio.Update(datosDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index", "Home");

        }

        // GET: Establecimiento/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Establecimiento/Delete/5
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
        //==========================================Metodos

        private EstablecimientoDto CargarDatos(EstablecimientoViewModel vm)
        {
            return new EstablecimientoDto()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Direccion = vm.Direccion,
                Email = vm.Email,
                Facebook = vm.Facebook,
                Instagram = vm.Instagram,
                Telefono = vm.Telefono,
                Profesional = vm.Profesional,
                Horario = vm.Horario,
                Twitter = vm.Twitter
            };
        }
    }
}
