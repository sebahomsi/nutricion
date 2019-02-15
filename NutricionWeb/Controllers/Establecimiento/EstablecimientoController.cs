using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Establecimiento;
using Servicio.Interface.Establecimiento;

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
        public ActionResult Index()
        {
            return View();
        }

        // GET: Establecimiento/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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

        // GET: Establecimiento/Create
        public async Task<ActionResult> Create()
        {
            if (User.IsInRole("Paciente"))
            {
                var todo = await _establecimientoServicio.Get();
                if (todo != null)
                {
                    var establecimiento = await _establecimientoServicio.GetById(todo.First().Id);
                    return RedirectToAction("Details", new { id = establecimiento.Id });
                }
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se encontraron datos");

            }
            if (await _establecimientoServicio.EstablecimientoFlag())
            {
                var flagId = await _establecimientoServicio.Get();
                var establecimiento = await _establecimientoServicio.GetById(flagId.First().Id);

                return RedirectToAction("Details", new {id = establecimiento.Id});
            }

            return View(new EstablecimientoViewModel());
        }

        // POST: Establecimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EstablecimientoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);

                    await _establecimientoServicio.Add(datosDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index","Home");

        }

        // GET: Establecimiento/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
            catch(Exception ex)
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
