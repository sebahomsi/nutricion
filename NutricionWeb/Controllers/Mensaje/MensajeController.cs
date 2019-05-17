using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Mensaje;
using PagedList;
using Servicio.Interface.Establecimiento;
using Servicio.Interface.Mensaje;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Mensaje
{
    public class MensajeController : Controller
    {
        private readonly IMensajeServicio _mensajeServicio;
        private readonly IEstablecimientoServicio _establecimientoServicio;

        public MensajeController(IMensajeServicio mensajeServicio, IEstablecimientoServicio establecimientoServicio)
        {
            _mensajeServicio = mensajeServicio;
            _establecimientoServicio = establecimientoServicio;

        }
        // GET: Mensaje
        public async Task<ActionResult> IndexRecibidos(int? page)
        {
            var pageNumber = page ?? 1;

            var mensajesRecibidos = await _mensajeServicio.GetRecibidos(User.Identity.Name);

            var contador = 0;
            foreach (var mensaje in mensajesRecibidos)
            {
                if (mensaje.Visto == false)
                {
                    contador = contador + 1;
                }
            }

            ViewBag.Contador = contador;

            return View(mensajesRecibidos.Select(x => new MensajeViewModel
            {
                Id = x.Id,
                EmailReceptor = x.EmailReceptor,
                EmailEmisor = x.EmailEmisor,
                Cuerpo = x.Cuerpo,
                Motivo = x.Motivo,
                Visto = x.Visto
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        public async Task<ActionResult> IndexEnviados(int? page)
        {
            var pageNumber = page ?? 1;

            var mensajesRecibidos = await _mensajeServicio.GetEnviados(User.Identity.Name);

            return View(mensajesRecibidos.Select(x => new MensajeViewModel
            {
                Id = x.Id,
                EmailReceptor = x.EmailReceptor,
                EmailEmisor = x.EmailEmisor,
                Cuerpo = x.Cuerpo,
                Motivo = x.Motivo,
                Visto = x.Visto
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Mensaje/Details/5
        public async Task<ActionResult> Details(long id)
        {
            var mensaje = await _mensajeServicio.GetById(id);
            if (User.Identity.Name == mensaje.EmailReceptor)
            {
                await _mensajeServicio.ChangeVisto(mensaje.Id);
            }

            return View(new MensajeViewModel()
            {
                Id = mensaje.Id,
                EmailReceptor = mensaje.EmailReceptor,
                EmailEmisor = mensaje.EmailEmisor,
                Cuerpo = mensaje.Cuerpo,
                Motivo = mensaje.Motivo,
                Visto = mensaje.Visto
            });
        }

        // GET: Mensaje/Create
        public async Task<ActionResult> Create()
        {
            if (!User.IsInRole("Administrador"))
            {
                var contactos = await _establecimientoServicio.Get();
                var contacto = contactos.First();
                if(contacto == null) return RedirectToAction("Error", "Home");

                return View(new MensajeABMViewModel()
                {
                    EmailEmisor = User.Identity.Name,
                    EmailReceptor = contacto.Email
                });

            }
            return View(new MensajeABMViewModel()
            {
                EmailEmisor = User.Identity.Name
            });
        }

        // POST: Mensaje/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MensajeABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vm.EmailReceptor == null)
                    {
                        ModelState.AddModelError(string.Empty, "Error: Receptor Vacío.");
                        return View(vm);
                    }
                    var mensaje = CargarDatos(vm);
                    await _mensajeServicio.Add(mensaje);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("IndexEnviados");

        }

        // GET: Mensaje/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mensaje/Edit/5
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

        // GET: Mensaje/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mensaje/Delete/5
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

        //========================================Metodos
        private MensajeDto CargarDatos(MensajeABMViewModel vm)
        {
            return new MensajeDto()
            {
                Id = vm.Id,
                EmailReceptor = vm.EmailReceptor,
                EmailEmisor = vm.EmailEmisor,
                Cuerpo = vm.Cuerpo,
                Motivo = vm.Motivo,
                Visto = vm.Visto
            };
        }
    }
}
