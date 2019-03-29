﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.Pago;
using PagedList;
using Servicio.Interface.Paciente;
using Servicio.Interface.Pago;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Pago
{
    public class PagoController : Controller
    {
        private readonly IPagoServicio _pagoServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public PagoController(IPagoServicio pagoServicio, IPacienteServicio pacienteServicio)
        {
            _pagoServicio = pagoServicio;
            _pacienteServicio = pacienteServicio;
        }

        
        // GET: Pago
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var pagos =await _pagoServicio.GetByDate(DateTime.Today, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (pagos == null) return HttpNotFound();

            return View(pagos.Select(x=>new PagoViewModel()
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Concepto = x.Concepto,
                Monto = x.Monto,
                Fecha = x.Fecha,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Codigo = x.Codigo,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Pago/Details/5
        public async Task<ActionResult> Details(long id)
        {

            var pago = await _pagoServicio.GetById(id);

            if (pago == null) return HttpNotFound();

            return View(new PagoViewModel
            {
                Id = pago.Id,
                EstaEliminado = pago.EstaEliminado,
                Concepto = pago.Concepto,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                PacienteId = pago.PacienteId,
                PacienteStr = pago.PacienteStr,
                Codigo = pago.Codigo
            });
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            return View(new PagoViewModel());
        }

        // POST: Pago/Create
        [HttpPost]
        public async Task<ActionResult> Create(PagoViewModel vm)
        {
            try
            {
                var dto = CargarDatos(vm);
                await _pagoServicio.Add(dto);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        private PagoDto CargarDatos(PagoViewModel vm)
        {
           return new PagoDto()
           {
               Id = vm.Id,
               EstaEliminado = vm.EstaEliminado,
               Concepto = vm.Concepto,
               Codigo = vm.Codigo,
               PacienteId = vm.PacienteId,
               PacienteStr = vm.PacienteStr,
               Fecha = vm.Fecha,
               Monto = vm.Monto,
           };
        }

        // GET: Pago/Edit/5
        public async Task<ActionResult> Edit(long id)
        {
            var pago = await _pagoServicio.GetById(id);

            return View(new PagoViewModel
            {
                Id = pago.Id,
                EstaEliminado = pago.EstaEliminado,
                Concepto = pago.Concepto,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                PacienteId = pago.PacienteId,
                PacienteStr = pago.PacienteStr,
                Codigo = pago.Codigo
            });
        }

        // POST: Pago/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(PagoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = CargarDatos(vm);

                    await _pagoServicio.Update(dto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Pago/Delete/5
        public async Task<ActionResult> Delete(long id)
        {
            var pago = await _pagoServicio.GetById(id);

            if (pago == null) return HttpNotFound();

            return View(new PagoViewModel
            {
                Id = pago.Id,
                EstaEliminado = pago.EstaEliminado,
                Concepto = pago.Concepto,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                PacienteId = pago.PacienteId,
                PacienteStr = pago.PacienteStr,
                Codigo = pago.Codigo
            });
        }

        // POST: Pago/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PagoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _pagoServicio.Delete(vm.Id);
                }
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");
        }
        
    }
}