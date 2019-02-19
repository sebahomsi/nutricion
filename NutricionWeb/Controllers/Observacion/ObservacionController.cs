﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.Observacion;
using NutricionWeb.Models.Patologia;
using PagedList;
using Servicio.Interface.AlergiaIntolerancia;
using Servicio.Interface.Alimento;
using Servicio.Interface.Observacion;
using Servicio.Interface.Paciente;
using Servicio.Interface.Patologia;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Observacion
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class ObservacionController : Controller
    {
        private readonly IObservacionServicio _observacionServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public ObservacionController(IObservacionServicio observacionServicio, IPacienteServicio pacienteServicio)
        {
            _observacionServicio = observacionServicio;
            _pacienteServicio = pacienteServicio;
        }
        // GET: Observacion
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var observaciones =
                await _observacionServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return View(observaciones.Select(x=> new ObservacionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Fumador = x.Fumador,
                BebeAlcohol = x.BebeAlcohol,
                EstadoCivil = x.EstadoCivil,
                CantidadSuenio = x.CantidadSuenio,
                TuvoHijo = x.TuvoHijo,
                CantidadHijo = x.CantidadHijo,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        

        // GET: Observacion/Create
        public async Task<ActionResult> Create(long pacienteId)
        {
            return View(new ObservacionABMViewModel()
            {
                PacienteId = pacienteId
            });
        }

        // POST: Observacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ObservacionABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var observacionDto = CargarDatos(vm);
                    observacionDto.Codigo = await _observacionServicio.GetNextCode();
                    if (observacionDto.TuvoHijo == false)
                    {
                        observacionDto.CantidadHijo = "0";
                    }

                    await _observacionServicio.Add(observacionDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: Observacion/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var observacion = await _observacionServicio.GetById(id.Value);

            return View(new ObservacionABMViewModel()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.PacienteStr,
                Fumador = observacion.Fumador,
                BebeAlcohol = observacion.BebeAlcohol,
                EstadoCivil = observacion.EstadoCivil,
                CantidadSuenio = observacion.CantidadSuenio,
                TuvoHijo = observacion.TuvoHijo,
                CantidadHijo = observacion.CantidadHijo,
                Eliminado = observacion.Eliminado
            });
        }

        // POST: Observacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ObservacionABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var observacionDto = CargarDatos(vm);

                    await _observacionServicio.Update(observacionDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: Observacion/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var observacion = await _observacionServicio.GetById(id.Value);

            return View(new ObservacionViewModel()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.PacienteStr,
                Fumador = observacion.Fumador,
                BebeAlcohol = observacion.BebeAlcohol,
                EstadoCivil = observacion.EstadoCivil,
                CantidadSuenio = observacion.CantidadSuenio,
                TuvoHijo = observacion.TuvoHijo,
                CantidadHijo = observacion.CantidadHijo,
                Eliminado = observacion.Eliminado
            });
        }

        // POST: Observacion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ObservacionViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    await _observacionServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Observacion/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var observacion = await _observacionServicio.GetByPacienteId(id.Value);

            return View(new ObservacionViewModel()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.PacienteStr,
                Fumador = observacion.Fumador,
                BebeAlcohol = observacion.BebeAlcohol,
                EstadoCivil = observacion.EstadoCivil,
                CantidadSuenio = observacion.CantidadSuenio,
                TuvoHijo = observacion.TuvoHijo,
                CantidadHijo = observacion.CantidadHijo,
                Eliminado = observacion.Eliminado,
                Patologias = observacion.Patologias.Select(x => new PatologiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    Eliminado = x.Eliminado
                }).ToList(),
                AlergiasIntolerancias = observacion.AlergiasIntolerancias.Select(q => new AlergiaIntoleranciaViewModel()
                {
                    Id = q.Id,
                    Codigo = q.Codigo,
                    Descripcion = q.Descripcion,
                    Eliminado = q.Eliminado
                }).ToList(),
                Alimentos = observacion.Alimentos.Select(t => new AlimentoViewModel()
                {
                    Id = t.Id,
                    Codigo = t.Codigo,
                    Descripcion = t.Descripcion,
                    Eliminado = t.Eliminado
                }).ToList()
            });
        }

        //========================Metodos especiales

        private ObservacionDto CargarDatos(ObservacionABMViewModel vm)
        {
            return new ObservacionDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Fumador = vm.Fumador,
                BebeAlcohol = vm.BebeAlcohol,
                EstadoCivil = vm.EstadoCivil,
                CantidadSuenio = vm.CantidadSuenio,
                TuvoHijo = vm.TuvoHijo,
                CantidadHijo = vm.CantidadHijo,
                Eliminado = vm.Eliminado
            };
        }
    }
}
