using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Paciente;
using System;
using System.Linq;
using System.Net;
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

        public DatoAntropometricoController(IDatoAntropometricoServicio datoAntropometricoServicio, IPacienteServicio pacienteServicio)
        {
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _pacienteServicio = pacienteServicio;
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

            return PartialView(new DatoAntropometricoABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
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
            return RedirectToAction("DatosAntropometricosParcial", "Paciente", new { id = vm.PacienteId });
        }


        // GET: DatoAntropometrico/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
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
                PerimetroCuello = dato.PerimetroCuello
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
            return RedirectToAction("DatosAntropometricosParcial", "Paciente", new { id = vm.PacienteId });
        }

        // GET: DatoAntropometrico/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
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
                return PartialView(vm);
            }
            return RedirectToAction("DatosAntropometricosParcial", "Paciente", new { id = vm.PacienteId });
        }


        // GET: DatoAntropometrico/Details/5
        public async Task<ActionResult> Details(long? id)
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
            });
        }

        //===================================Metodos Privadox

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

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
            if (pacienteId == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

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

            };
        }
    }
}
