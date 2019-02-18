using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Paciente;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.DatoAntropometrico
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class DatoAntropometricoController : Controller
    {
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public DatoAntropometricoController(IDatoAntropometricoServicio datoAntropometricoServicio, IPacienteServicio pacienteServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _pacienteServicio = pacienteServicio;
        }

        // GET: DatoAntropometrico
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var datos = await _datoAntropometricoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar)
                ? cadenaBuscar
                : string.Empty);

            if (datos == null) return HttpNotFound();

            return View(datos.Select(x=> new DatoAntropometricoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Altura = x.Altura,
                FechaMedicion = x.FechaMedicion,
                MasaGrasa = x.MasaGrasa,
                MasaCorporal = x.MasaCorporal,
                Peso = x.Peso,
                PerimetroCintura = x.PerimetroCintura,
                PerimetroCadera = x.PerimetroCadera,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: DatoAntropometrico/Create
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create()
        {
            return View(new DatoAntropometricoABMViewModel());
        }

        // POST: DatoAntropometrico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DatoAntropometricoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);
                    datosDto.Codigo = await _datoAntropometricoServicio.GetNextCode();

                    await _datoAntropometricoServicio.Add(datosDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: DatoAntropometrico/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return View(new DatoAntropometricoABMViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                Peso = dato.Peso,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado
            });
        }

        // POST: DatoAntropometrico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DatoAntropometricoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);
                    await _datoAntropometricoServicio.Update(datosDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: DatoAntropometrico/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return View(new DatoAntropometricoViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                Peso = dato.Peso,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado
            });
        }

        // POST: DatoAntropometrico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DatoAntropometricoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _datoAntropometricoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }


        // GET: DatoAntropometrico/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return View(new DatoAntropometricoViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                Peso = dato.Peso,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado
            });
        }

        //===================================Metodos Privadox

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var pacientes =
                await _pacienteServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return HttpNotFound();

            return PartialView(pacientes.Select(x => new PacienteViewModel() 
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Direccion = x.Direccion,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));

        }

        public async Task<ActionResult> TraerPaciente(long? pacienteId)
        {
            if (pacienteId == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

            return Json(paciente, JsonRequestBehavior.AllowGet);
        }

        private DatoAntropometricoDto CargarDatos(DatoAntropometricoABMViewModel vm)
        {
            return new DatoAntropometricoDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Altura = vm.Altura,
                FechaMedicion = vm.FechaMedicion,
                MasaGrasa = vm.MasaGrasa,
                MasaCorporal = vm.MasaCorporal,
                Peso = vm.Peso,
                PerimetroCintura = vm.PerimetroCintura,
                PerimetroCadera = vm.PerimetroCadera,
                Eliminado = vm.Eliminado
            };
        }
    }
}
