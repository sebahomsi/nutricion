using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Establecimiento;
using Servicio.Interface.Establecimiento;

namespace NutricionWeb.Controllers.Establecimiento
{
    public class EstablecimientoController : Controller
    {
        // GET: Establecimiento
        public ActionResult Index()
        {
            return View();
        }

        // GET: Establecimiento/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Establecimiento/Create
        public async Task<ActionResult> Create()
        {
            return View(new EstablecimientoViewModel());
        }

        // POST: Establecimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EstablecimientoViewModel vm)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Establecimiento/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: Establecimiento/Edit/5
        [HttpPost]
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
