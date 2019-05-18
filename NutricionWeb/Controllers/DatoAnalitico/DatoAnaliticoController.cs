using NutricionWeb.Models.DatoAnalitico;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.Paciente;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.DatoAnalitico
{
    [Authorize(Roles = "Administrador, Empleado")]
    public class DatoAnaliticoController : ControllerBase
    {
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IDatoAnaliticoServicio _datoAnaliticoServicio;

        public DatoAnaliticoController(IPacienteServicio pacienteServicio, IDatoAnaliticoServicio datoAnaliticoServicio)
        {
            _pacienteServicio = pacienteServicio;
            _datoAnaliticoServicio = datoAnaliticoServicio;
        }

        // GET: DatoAnalitico
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var datos = await _datoAnaliticoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar)
                ? cadenaBuscar
                : string.Empty);

            if (datos == null) RedirectToAction("Error", "Home");

            return View(datos.Select(x => new DatoAnaliticoViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                ColesterolHdl = x.ColesterolHdl,
                ColesterolLdl = x.ColesterolLdl,
                ColesterolTotal = x.ColesterolTotal,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                PresionDiastolica = x.PresionDiastolica,
                PresionSistolica = x.PresionSistolica,
                Trigliceridos = x.Trigliceridos,
                FechaMedicion = x.FechaMedicion,
                Eliminado = x.Eliminado
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: DatoAnalitico/Create
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create()
        {
            return View(new DatoAnaliticoABMViewModel());
        }

        // POST: DatoAnalitico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DatoAnaliticoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);

                    datosDto.Codigo = await _datoAnaliticoServicio.GetNextCode();

                    await _datoAnaliticoServicio.Add(datosDto);
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
            if (id == null) RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new DatoAnaliticoABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(DatoAnaliticoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);
                    datosDto.Codigo = await _datoAnaliticoServicio.GetNextCode();

                    await _datoAnaliticoServicio.Add(datosDto);
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
            return RedirectToAction("DatosAnaliticosParcial", "Paciente", new { id = vm.PacienteId });

        }

        // GET: DatoAnalitico/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _datoAnaliticoServicio.GetById(id.Value);

            return View(new DatoAnaliticoABMViewModel()
            {
                Id = datos.Id,
                Codigo = datos.Codigo,
                ColesterolHdl = datos.ColesterolHdl,
                ColesterolLdl = datos.ColesterolLdl,
                ColesterolTotal = datos.ColesterolTotal,
                PacienteId = datos.PacienteId,
                PacienteStr = datos.PacienteStr,
                PresionDiastolica = datos.PresionDiastolica,
                PresionSistolica = datos.PresionSistolica,
                Trigliceridos = datos.Trigliceridos,
                FechaMedicion = datos.FechaMedicion,
                Eliminado = datos.Eliminado
            });
        }

        // POST: DatoAnalitico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DatoAnaliticoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var datos = CargarDatos(vm);
                    await _datoAnaliticoServicio.Update(datos);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: DatoAnalitico/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _datoAnaliticoServicio.GetById(id.Value);

            return View(new DatoAnaliticoViewModel()
            {
                Id = datos.Id,
                Codigo = datos.Codigo,
                ColesterolHdl = datos.ColesterolHdl,
                ColesterolLdl = datos.ColesterolLdl,
                ColesterolTotal = datos.ColesterolTotal,
                PacienteId = datos.PacienteId,
                PacienteStr = datos.PacienteStr,
                PresionDiastolica = datos.PresionDiastolica,
                PresionSistolica = datos.PresionSistolica,
                Trigliceridos = datos.Trigliceridos,
                FechaMedicion = datos.FechaMedicion,
                Eliminado = datos.Eliminado
            });
        }

        // POST: DatoAnalitico/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(DatoAnaliticoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _datoAnaliticoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        // GET: DatoAnalitico/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _datoAnaliticoServicio.GetById(id.Value);

            return View(new DatoAnaliticoViewModel()
            {
                Id = datos.Id,
                Codigo = datos.Codigo,
                ColesterolHdl = datos.ColesterolHdl,
                ColesterolLdl = datos.ColesterolLdl,
                ColesterolTotal = datos.ColesterolTotal,
                PacienteId = datos.PacienteId,
                PacienteStr = datos.PacienteStr,
                PresionDiastolica = datos.PresionDiastolica,
                PresionSistolica = datos.PresionSistolica,
                Trigliceridos = datos.Trigliceridos,
                FechaMedicion = datos.FechaMedicion,
                Eliminado = datos.Eliminado
            });
        }


        //=================================Metodos Especiales

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;
            var eliminado = false;
            var pacientes =
                await _pacienteServicio.Get(establecimientoId, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

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

        public ActionResult CalcularColesterolTotal(string hdl, string ldl, string tri)
        {
            var total = 0m;

            if (string.IsNullOrEmpty(hdl) || string.IsNullOrEmpty(ldl) || string.IsNullOrEmpty(tri))
                return Json(total, JsonRequestBehavior.AllowGet);
            var colHdl = decimal.Parse(hdl);
            var colLdl = decimal.Parse(ldl);
            var colTri = decimal.Parse(tri);

            total = colHdl + colLdl + (colTri / 5);

            return Json(total, JsonRequestBehavior.AllowGet);

        }

        private DatoAnaliticoDto CargarDatos(DatoAnaliticoABMViewModel vm)
        {
            return new DatoAnaliticoDto()
            {
                Id = vm.Id,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Codigo = vm.Codigo,
                ColesterolHdl = vm.ColesterolHdl,
                ColesterolLdl = vm.ColesterolLdl,
                PresionDiastolica = vm.PresionDiastolica,
                PresionSistolica = vm.PresionSistolica,
                Trigliceridos = vm.Trigliceridos,
                ColesterolTotal = vm.ColesterolTotal,
                FechaMedicion = vm.FechaMedicion,
                Eliminado = vm.Eliminado
            };
        }
    }
}
