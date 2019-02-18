using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Infraestructura.Contexto;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.Turno;
using PagedList;
using Servicio.Interface.Paciente;
using Servicio.Interface.Turno;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Turno
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class TurnoController : Controller
    {
        private readonly ITurnoServicio _turnoServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public TurnoController(ITurnoServicio turnoServicio, IPacienteServicio pacienteServicio)
        {
            _turnoServicio = turnoServicio;
            _pacienteServicio = pacienteServicio;
        }

        // GET: Turno
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var turnos = await _turnoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (turnos == null) return HttpNotFound();

            return View(turnos.Select(x=> new TurnoViewModel()
            {
                Id = x.Id,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                HorarioEntrada = x.HorarioEntrada,
                HorarioSalida = x.HorarioSalida,
                Motivo = x.Motivo,
                Numero = x.Numero,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        

        // GET: Turno/Create
        public async Task<ActionResult> Create()
        {
            return View(new TurnoABMViewModel());
        }

        // POST: Turno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TurnoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vm.HorarioEntrada > vm.HorarioSalida)
                    {
                        ModelState.AddModelError(string.Empty, "El campo Horario de Entrada no puede ser mayor al de Salida");
                        return View(vm);
                    }
                    var turnos = await _turnoServicio.Get(string.Empty);
                    foreach (var turno in turnos)
                    {
                        if (turno.HorarioEntrada < vm.HorarioEntrada && turno.HorarioSalida > vm.HorarioEntrada)
                        {
                            ModelState.AddModelError(string.Empty, "Ya existe un turno para ese rango de horarios");
                            return View(vm);
                        }
                    }
                    var turnoDto = CargarDatos(vm);
                    turnoDto.Numero = await _turnoServicio.GetNextCode();

                    await _turnoServicio.Add(turnoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("About","Home");

        }

        // GET: Turno/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var turno = await _turnoServicio.GetById(id.Value);

            return View(new TurnoABMViewModel()
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.PacienteStr,
                HorarioEntrada = turno.HorarioEntrada,
                HorarioSalida = turno.HorarioSalida,
                Motivo = turno.Motivo,
                Numero = turno.Numero,
                Eliminado = turno.Eliminado
            });
        }

        // POST: Turno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TurnoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var turnoDto = CargarDatos(vm);

                    await _turnoServicio.Update(turnoDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        // GET: Turno/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var turno = await _turnoServicio.GetById(id.Value);

            return View(new TurnoViewModel()
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.PacienteStr,
                HorarioEntrada = turno.HorarioEntrada,
                HorarioSalida = turno.HorarioSalida,
                Motivo = turno.Motivo,
                Numero = turno.Numero,
                Eliminado = turno.Eliminado
            });
        }

        // POST: Turno/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(TurnoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _turnoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: Turno/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var turno = await _turnoServicio.GetById(id.Value);

            return View(new TurnoViewModel()
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.PacienteStr,
                HorarioEntrada = turno.HorarioEntrada,
                HorarioSalida = turno.HorarioSalida,
                Motivo = turno.Motivo,
                Numero = turno.Numero,
                Eliminado = turno.Eliminado
            });
        }

        //====================================Metodos Hugangelion
        public async Task<JsonResult> GetTurnos()
        {
            var events = await _turnoServicio.Get(string.Empty);
            
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        
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

        private TurnoDto CargarDatos(TurnoABMViewModel vm)
        {
            return new TurnoDto()
            {
                Id = vm.Id,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Numero = vm.Numero,
                HorarioEntrada = vm.HorarioEntrada,
                HorarioSalida = vm.HorarioSalida,
                Motivo = vm.Motivo,
                Eliminado = vm.Eliminado
            };
        }
    }
}
