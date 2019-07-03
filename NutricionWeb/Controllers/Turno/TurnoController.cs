using AutoMapper;
using NutricionWeb.Models.Estado;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.Turno;
using PagedList;
using Servicio.Interface.Estado;
using Servicio.Interface.Paciente;
using Servicio.Interface.Turno;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Turno
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoServicio _turnoServicio;
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IEstadoServicio _estadoServicio;

        public TurnoController(ITurnoServicio turnoServicio, IPacienteServicio pacienteServicio, IEstadoServicio estadoServicio)
        {
            _turnoServicio = turnoServicio;
            _pacienteServicio = pacienteServicio;
            _estadoServicio = estadoServicio;
        }

        // GET: Turno
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var turnos = await _turnoServicio.Get(establecimientoId, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (turnos == null) return RedirectToAction("Error", "Home");

            return View(turnos.Select(x => new TurnoViewModel()
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
            return await Task.Run(() => View(new TurnoABMViewModel()));
        }

        // POST: Turno/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TurnoABMViewModel vm)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();
            try
            {
                vm = ModificarFechas(vm);
                if (ModelState.IsValid)
                {
                    if (vm.HorarioEntradaDateTime.Date > vm.HorarioSalidaDateTime.Date)
                    {
                        ModelState.AddModelError(string.Empty, "El campo Horario de Entrada no puede ser mayor al de Salida");

                        return View(vm);
                    }

                    var turnos = await _turnoServicio.Get(establecimientoId, false, string.Empty);

                    foreach (var turno in turnos)
                    {
                        if (turno.HorarioEntrada.Date == vm.HorarioEntradaDateTime.Date)
                        {
                            if (turno.HorarioEntrada.TimeOfDay < vm.HorarioEntradaDateTime.TimeOfDay &&
                                vm.HorarioEntradaDateTime.TimeOfDay < turno.HorarioSalida.TimeOfDay)
                            {
                                ModelState.AddModelError(string.Empty, "Ya existe un turno para ese rango de horarios");

                                return View(vm);
                            }
                        }
                    }
                    var turnoDto = CargarDatos(vm);
                    turnoDto.Numero = await _turnoServicio.GetNextCode();

                    await _turnoServicio.Add(turnoDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("About", "Home");

        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new TurnoABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(TurnoABMViewModel vm)
        {
            try
            {
                vm = ModificarFechas(vm);
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);
                    datosDto.Numero = await _turnoServicio.GetNextCode();

                    await _turnoServicio.Add(datosDto);
                }
                else
                {
                    return PartialView(vm);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("TurnosParcial", "Paciente", new { id = vm.PacienteId });

        }

        // GET: Turno/Edit/5
        public async Task<ActionResult> Edit(long? id, int? volver)
        {
            ViewBag.Volver = volver ?? 0;

            if (id == null) return RedirectToAction("Error", "Home");

            var turno = await _turnoServicio.GetById(id.Value);

            return View(new TurnoABMViewModel()
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.PacienteStr,
                HorarioEntradaDateTime = turno.HorarioEntrada,
                HorarioSalidaDateTime = turno.HorarioSalida,
                HorarioEntrada = turno.HorarioEntrada.ToString("dd/MM/yyyy HH:mm"),
                HorarioSalida = turno.HorarioSalida.ToString("dd/MM/yyyy HH:mm"),
                Motivo = turno.Motivo,
                Numero = turno.Numero,
                Eliminado = turno.Eliminado,
                EstadoId = turno.EstadoId,
                EstadoDescripcion = turno.EstadoDescripcion
            });
        }

        // POST: Turno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TurnoABMViewModel vm, int? volver)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();
            ViewBag.Volver = volver;
            try
            {
                vm = ModificarFechas(vm);
                if (ModelState.IsValid)
                {
                    if (vm.HorarioEntradaDateTime > vm.HorarioSalidaDateTime)
                    {
                        ModelState.AddModelError(string.Empty, "El campo Horario de Entrada no puede ser mayor al de Salida");

                        return View(vm);
                    }

                    var turnos = await _turnoServicio.Get(establecimientoId, false, string.Empty);

                    foreach (var turno in turnos)
                    {
                        if (turno.HorarioEntrada.Date == vm.HorarioEntradaDateTime.Date)
                        {
                            if (turno.HorarioEntrada.TimeOfDay < vm.HorarioEntradaDateTime.TimeOfDay &&
                                vm.HorarioEntradaDateTime.TimeOfDay < turno.HorarioSalida.TimeOfDay)
                            {
                                ModelState.AddModelError(string.Empty, "Ya existe un turno para ese rango de horarios");

                                return View(vm);
                            }
                        }
                    }
                    var turnoDto = CargarDatos(vm);

                    await _turnoServicio.Update(turnoDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return volver == 0 || volver == null ? RedirectToAction("Index") : RedirectToAction("About", "Home");
        }

        public async Task<ActionResult> EditParcial(long? id, int? volver)
        {
            ViewBag.Volver = volver ?? 0;

            if (id == null) return RedirectToAction("Error", "Home");

            var turno = await _turnoServicio.GetById(id.Value);

            return PartialView(new TurnoABMViewModel()
            {
                Id = turno.Id,
                PacienteId = turno.PacienteId,
                PacienteStr = turno.PacienteStr,
                HorarioEntradaDateTime = turno.HorarioEntrada,
                HorarioSalidaDateTime = turno.HorarioSalida,
                HorarioEntrada = turno.HorarioEntrada.ToString("dd/MM/yyyy HH:mm"),
                HorarioSalida = turno.HorarioSalida.ToString("dd/MM/yyyy HH:mm"),
                Motivo = turno.Motivo,
                Numero = turno.Numero,
                Eliminado = turno.Eliminado,
                EstadoId = turno.EstadoId,
                EstadoDescripcion = turno.EstadoDescripcion
            });
        }

        // POST: Turno/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditParcial(TurnoABMViewModel vm, int? volver)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();
            ViewBag.Volver = volver;
            try
            {
                vm = ModificarFechas(vm);
                if (ModelState.IsValid)
                {
                    if (vm.HorarioEntradaDateTime > vm.HorarioSalidaDateTime)
                    {
                        ModelState.AddModelError(string.Empty, "El campo Horario de Entrada no puede ser mayor al de Salida");

                        return PartialView(vm);
                    }

                    var turnos = await _turnoServicio.Get(establecimientoId, false, string.Empty);

                    foreach (var turno in turnos)
                    {
                        if (turno.HorarioEntrada.Date == vm.HorarioEntradaDateTime.Date)
                        {
                            if (turno.HorarioEntrada.TimeOfDay < vm.HorarioEntradaDateTime.TimeOfDay &&
                                vm.HorarioEntradaDateTime.TimeOfDay < turno.HorarioSalida.TimeOfDay)
                            {
                                ModelState.AddModelError(string.Empty, "Ya existe un turno para ese rango de horarios");

                                return PartialView(vm);
                            }
                        }
                    }
                    var turnoDto = CargarDatos(vm);

                    await _turnoServicio.Update(turnoDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }

            return volver == 0 || volver == null ? RedirectToAction("TurnosParcial", "Paciente", new { id = vm.PacienteId }) : RedirectToAction("About", "Home");
        }

        // GET: Turno/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var turno = await _turnoServicio.GetById(id.Value);

            return PartialView(new TurnoViewModel()
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
        public async Task<ActionResult> DeleteParcial(TurnoViewModel vm)
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
                return PartialView(vm);
            }
            return RedirectToAction("TurnosParcial", "Paciente", new { id = vm.PacienteId });
        }

        // GET: Turno/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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

        public async Task<ActionResult> DetailsParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var turno = await _turnoServicio.GetById(id.Value);

            return PartialView(new TurnoViewModel()
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

        private static TurnoABMViewModel ModificarFechas(TurnoABMViewModel vm)
        {
            var horaEntrada = vm.HorarioEntrada.Split('/');

            var horaSalida = vm.HorarioSalida.Split('/');

            var c = new CultureInfo("en-US");

            vm.HorarioEntradaDateTime = DateTime.Parse($"{horaEntrada[1]}/{horaEntrada[0]}/{horaEntrada[2]}", c);

            vm.HorarioSalidaDateTime = DateTime.Parse($"{horaSalida[1]}/{horaSalida[0]}/{horaSalida[2]}", c);

            // Asi estaban antes
            //vm.HorarioEntradaDateTime = DateTime.Parse($"{horaEntrada[1]}/{horaEntrada[0]}/{horaEntrada[2]}");
            //vm.HorarioSalidaDateTime = DateTime.Parse($"{horaSalida[1]}/{horaSalida[0]}/{horaSalida[2]}");

            return vm;
        }

        public async Task<JsonResult> GetTurnos()
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var turnos = await _turnoServicio.Get(establecimientoId, false, string.Empty);

            var events = turnos.Select(x=> new
            {
                x.Id,
                x.PacienteStr,
                Fecha = x.HorarioEntrada.ToString("yyyy-MM-dd"),
                Hora = x.HorarioEntrada.ToString("HH:mm", CultureInfo.InvariantCulture),
                x.EstadoColor,
                x.Motivo,
                x.PacienteId
            });

            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public async Task<ActionResult> BuscarEstado(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var datos = await _estadoServicio.Get(false, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            var estados = Mapper.Map<IEnumerable<EstadoViewModel>>(datos);

            return PartialView(estados.ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }


        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();
            ViewBag.FilterValue = cadenaBuscar;

            var pageNumber = page ?? 1;

            var pacientes =
                await _pacienteServicio.Get(establecimientoId, false, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return RedirectToAction("Error", "Home");

            return PartialView(pacientes.Select(x => new PacienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Cuit = x.Cuit,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                FechaAlta = x.FechaAlta,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));

        }

        public async Task<ActionResult> TraerPaciente(long? pacienteId)
        {
            if (pacienteId == null) return RedirectToAction("Error", "Home");

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
                EstadoId = vm.EstadoId,
                HorarioEntrada = vm.HorarioEntradaDateTime,
                HorarioSalida = vm.HorarioSalidaDateTime,
                Motivo = vm.Motivo,
                Eliminado = vm.Eliminado
            };
        }

        public async Task<ActionResult> TraerEstado(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var estado = await _estadoServicio.GetById(id.Value);

            return Json(estado, JsonRequestBehavior.AllowGet);
        }
    }
}
