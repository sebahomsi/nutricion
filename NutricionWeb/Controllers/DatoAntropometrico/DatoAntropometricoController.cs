using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Paciente;
using Servicio.Interface.Turno;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.File;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.DatoAntropometrico
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class DatoAntropometricoController : ControllerBase
    {
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;
        private readonly IPacienteServicio _pacienteServicio;
        private readonly ITurnoServicio _turnoServicio;

        public DatoAntropometricoController(IDatoAntropometricoServicio datoAntropometricoServicio, IPacienteServicio pacienteServicio, ITurnoServicio turnoServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _pacienteServicio = pacienteServicio;
            _turnoServicio = turnoServicio;
        }

        // GET: DatoAntropometrico
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var datos = await _datoAntropometricoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar)
                ? cadenaBuscar
                : string.Empty);

            if (datos == null) return RedirectToAction("Error", "Home");

            return View(datos.Select(x => new DatoAntropometricoViewModel()
            {

                Id = x.Id,
                Codigo = x.Codigo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Altura = x.Altura,
                FechaMedicion = x.FechaMedicion,
                MasaGrasa = x.MasaGrasa,
                MasaCorporal = x.MasaCorporal,
                PesoActual = x.PesoActual,
                PerimetroCintura = x.PerimetroCintura,
                PerimetroCadera = x.PerimetroCadera,
                Eliminado = x.Eliminado,
                PesoHabitual = x.PesoHabitual,
                PesoIdeal = x.PesoIdeal,
                PesoDeseado = x.PesoDeseado,
                PliegueSuprailiaco = x.PliegueSuprailiaco,
                PliegueMuslo = x.PliegueMuslo,
                PlieguePierna = x.PlieguePierna,
                PliegueSubescapular = x.PliegueSubescapular,
                PliegueTriceps = x.PliegueTriceps,
                PliegueAbdominal = x.PliegueAbdominal,
                TotalPliegues = x.TotalPliegues
            }).OrderBy(x=>x.FechaMedicion).ToPagedList(pageNumber, CantidadFilasPorPaginas));
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
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";
                    var datosDto = CargarDatos(vm, pic);
                    datosDto.Codigo = await _datoAntropometricoServicio.GetNextCode();

                    await _datoAntropometricoServicio.Add(datosDto);
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
        public async Task<ActionResult> CreateParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);
            var mediciones = await _datoAntropometricoServicio.GetByIdPaciente(id.Value);
            var ultimaAgregada=mediciones.Where(x=>!x.Eliminado).OrderByDescending(x => x.FechaMedicion).FirstOrDefault();

            return PartialView(new DatoAntropometricoABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}",
                FechaMedicion = DateTime.Now,
                Altura = ultimaAgregada!=null? ultimaAgregada.Altura:"",
                
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(DatoAntropometricoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";
                    var datosDto = CargarDatos(vm, pic);
                    datosDto.Codigo = await _datoAntropometricoServicio.GetNextCode();

                    await _datoAntropometricoServicio.Add(datosDto);
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
            return RedirectToAction("Index");
        }


        // GET: DatoAntropometrico/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
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
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";
                    var datosDto = CargarDatos(vm, pic);
                    await _datoAntropometricoServicio.Update(datosDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EditParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return PartialView(new DatoAntropometricoABMViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
            });
        }

        // POST: DatoAntropometrico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditParcial(DatoAntropometricoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";
                    var datosDto = CargarDatos(vm, pic);
                    await _datoAntropometricoServicio.Update(datosDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("DatosAntropometricosParcial", "Paciente", new { id = vm.PacienteId });
        }

        // GET: DatoAntropometrico/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                FotoStr = dato.Foto,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
                TotalPliegues = dato.TotalPliegues
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

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return PartialView(new DatoAntropometricoViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                FotoStr = dato.Foto,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
                TotalPliegues = dato.TotalPliegues
            });
        }

        // POST: DatoAntropometrico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteParcial(DatoAntropometricoViewModel vm)
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
                return PartialView(vm);
            }
            return RedirectToAction("DatosAntropometricosParcial", "Paciente", new { id = vm.PacienteId });
        }

        // GET: DatoAntropometrico/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

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
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                FotoStr = dato.Foto,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
                TotalPliegues = dato.TotalPliegues
            });
        }

        public async Task<ActionResult> DetailsParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _datoAntropometricoServicio.GetById(id.Value);

            return PartialView(new DatoAntropometricoViewModel()
            {
                Id = dato.Id,
                Codigo = dato.Codigo,
                PacienteId = dato.PacienteId,
                PacienteStr = dato.PacienteStr,
                Altura = dato.Altura,
                FechaMedicion = dato.FechaMedicion,
                MasaGrasa = dato.MasaGrasa,
                MasaCorporal = dato.MasaCorporal,
                PesoActual = dato.PesoActual,
                PerimetroCintura = dato.PerimetroCintura,
                PerimetroCadera = dato.PerimetroCadera,
                Eliminado = dato.Eliminado,
                PesoHabitual = dato.PesoHabitual,
                PesoIdeal = dato.PesoIdeal,
                PesoDeseado = dato.PesoDeseado,
                PerimetroCuello = dato.PerimetroCuello,
                FotoStr = dato.Foto,
                PliegueSuprailiaco = dato.PliegueSuprailiaco,
                PliegueMuslo = dato.PliegueMuslo,
                PlieguePierna = dato.PlieguePierna,
                PliegueSubescapular = dato.PliegueSubescapular,
                PliegueTriceps = dato.PliegueTriceps,
                PliegueAbdominal = dato.PliegueAbdominal,
                TotalPliegues = dato.TotalPliegues
            });
        }

        //===================================Metodos Privadox

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;

            ViewBag.FilterValue = cadenaBuscar;

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

        public async Task<ActionResult> TraerPaciente(long? pacienteId, bool? traerTurno)
        {
            if (pacienteId == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

            if (traerTurno.HasValue)
            {
                var turno = await _turnoServicio.GetLastByPacienteId(pacienteId.Value);
                var fecha = turno.HorarioEntrada.ToString("dd/MM/yyyy HH:mm");
                return Json(new { paciente, fecha }, JsonRequestBehavior.AllowGet);
            }

            return Json(paciente, JsonRequestBehavior.AllowGet);
        }

        private DatoAntropometricoDto CargarDatos(DatoAntropometricoABMViewModel vm, string pic)
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
                PesoActual = vm.PesoActual,
                PerimetroCintura = vm.PerimetroCintura,
                PerimetroCadera = vm.PerimetroCadera,
                Eliminado = vm.Eliminado,
                Foto = pic,
                PesoHabitual = vm.PesoHabitual,
                PesoIdeal = vm.PesoIdeal,
                PesoDeseado = vm.PesoDeseado,
                PerimetroCuello = vm.PerimetroCuello,
                PliegueSuprailiaco = vm.PliegueSuprailiaco,
                PliegueMuslo = vm.PliegueMuslo,
                PlieguePierna = vm.PlieguePierna,
                PliegueSubescapular = vm.PliegueSubescapular,
                PliegueTriceps = vm.PliegueTriceps,
                PliegueAbdominal = vm.PliegueAbdominal,

            };
        }
    }
}
