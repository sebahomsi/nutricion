using AutoMapper;
using NutricionWeb.Helpers.Establecimiento;
using NutricionWeb.Helpers.Persona;
using NutricionWeb.Models.Anamnesis;
using NutricionWeb.Models.DatoAnalitico;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Estrategia;
using NutricionWeb.Models.Objetivo;
using NutricionWeb.Models.Observacion;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using NutricionWeb.Models.Turno;
using PagedList;
using Servicio.Interface.Anamnesis;
using Servicio.Interface.DatoAnalitico;
using Servicio.Interface.DatoAntropometrico;
using Servicio.Interface.Estrategia;
using Servicio.Interface.Objetivo;
using Servicio.Interface.Observacion;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using Servicio.Interface.Turno;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.File;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Paciente
{

    public class PacienteController : ControllerBase
    {
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IComboBoxSexo _comboBoxSexo;
        private readonly IDatoAnaliticoServicio _datoAnaliticoServicio;
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IDatoAntropometricoServicio _datoAntropometricoServicio;
        private readonly ITurnoServicio _turnoServicio;
        private readonly IObjetivoServicio _objetivoServicio;
        private readonly IAnamnesisServicio _anamnesisServicio;
        private readonly IEstrategiaServicio _estrategiaServicio;
        private readonly IObservacionServicio _observacionServicio;
        private readonly IComboBoxEstablecimiento _comboBoxEstablecimiento;


        public PacienteController(IPacienteServicio pacienteServicio,
            IComboBoxSexo comboBoxSexo,
            IDatoAnaliticoServicio datoAnaliticoServicio,
            IPlanAlimenticioServicio planAlimenticioServicio,
            IDatoAntropometricoServicio datoAntropometricoServicio,
            ITurnoServicio turnoServicio, IObjetivoServicio objetivoServicio,
            IAnamnesisServicio anamnesisServicio,
            IEstrategiaServicio estrategiaServicio,
            IComboBoxEstablecimiento comboBoxEstablecimiento, IObservacionServicio observacionServicio)
        {
            _pacienteServicio = pacienteServicio;
            _comboBoxSexo = comboBoxSexo;
            _datoAnaliticoServicio = datoAnaliticoServicio;
            _planAlimenticioServicio = planAlimenticioServicio;
            _datoAntropometricoServicio = datoAntropometricoServicio;
            _turnoServicio = turnoServicio;
            _objetivoServicio = objetivoServicio;
            _anamnesisServicio = anamnesisServicio;
            _estrategiaServicio = estrategiaServicio;
            _observacionServicio = observacionServicio;
            _comboBoxEstablecimiento = comboBoxEstablecimiento;
        }


        // GET: Paciente
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;
            var pacientes = await _pacienteServicio.Get(establecimientoId, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return RedirectToAction("Error", "Home");

            return View(pacientes.Select(x => new PacienteViewModel()
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
            }).OrderByDescending(fa => fa.FechaAlta).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: Paciente/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Create()
        {
            return View(new PacienteABMViewModel()
            {
                Sexos = await _comboBoxSexo.Poblar(),
                Establecimientos = await _comboBoxEstablecimiento.Poblar(),
                EstablecimientoId = ObtenerEstablecimientoIdUser() ?? -1
            });
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PacienteABMViewModel vm)
        {
            try
            {
                vm = ModificarFechas(vm);
                var modelErrors = new List<string>();
                if (ModelState.IsValid)
                {
                    var pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    vm.FechaAlta = DateTime.Now;

                    var pacienteDto = CargarDatos(vm, pic);

                    pacienteDto.Codigo = await _pacienteServicio.GetNextCode();

                    await _pacienteServicio.Add(pacienteDto);
                }
                else
                {
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                vm.Establecimientos = await _comboBoxEstablecimiento.Poblar();
                vm.Sexos = await _comboBoxSexo.Poblar();
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return View(new PacienteABMViewModel()
            {
                Sexos = await _comboBoxSexo.Poblar(),
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Telefono = paciente.Telefono,
                Cuit = paciente.Cuit,
                Dni = paciente.Dni,
                FechaNacDateTime = paciente.FechaNac,
                FechaNac = paciente.FechaNac.ToString("dd/MM/yyyy"),
                FechaAlta = paciente.FechaAlta,
                Sexo = paciente.Sexo,
                Mail = paciente.Mail,
                Eliminado = paciente.Eliminado,
            });
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PacienteABMViewModel vm)
        {
            try
            {
                vm = ModificarFechas(vm);
                if (ModelState.IsValid)
                {
                    var pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    var pacienteDto = CargarDatos(vm, pic);

                    await _pacienteServicio.Update(pacienteDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                vm.Establecimientos = await _comboBoxEstablecimiento.Poblar();
                vm.Sexos = await _comboBoxSexo.Poblar();
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return View(new PacienteViewModel()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Telefono = paciente.Telefono,
                Cuit = paciente.Cuit,
                Dni = paciente.Dni,
                FechaNac = paciente.FechaNac.Date,
                Sexo = paciente.Sexo,
                FotoStr = paciente.Foto,
                Mail = paciente.Mail,
                Eliminado = paciente.Eliminado,
            });
        }

        // POST: Paciente/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PacienteViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _pacienteServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Details/5
        [Authorize(Roles = "Administrador, Empleado, Paciente")]
        public async Task<ActionResult> Details(long? id, string email = "")
        {
            PacienteDto paciente;
            if (!string.IsNullOrEmpty(email))
            {
                paciente = await _pacienteServicio.GetByEmail(email);

                if (paciente == null) return RedirectToAction("Error", "Home");
            }
            else
            {
                if (id == null) return RedirectToAction("Error", "Home");

                paciente = await _pacienteServicio.GetById(id.Value);
            }

            return View(new PacienteViewModel()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Telefono = paciente.Telefono,
                Cuit = paciente.Cuit,
                Dni = paciente.Dni,
                FechaNac = paciente.FechaNac.Date,
                FechaAlta = paciente.FechaAlta,
                Sexo = paciente.Sexo,
                Mail = paciente.Mail,
                FotoStr = paciente.Foto,
                Eliminado = paciente.Eliminado,
                TieneObservacion = paciente.TieneObservacion
            });
        }

        //===============================================================================//
        private PacienteDto CargarDatos(PacienteABMViewModel vm, string pic)
        {
            return new PacienteDto()
            {
                Id = vm.Id,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Mail = vm.Mail,
                Dni = vm.Dni,
                Celular = vm.Celular,
                Telefono = vm.Telefono,
                Cuit = vm.Cuit,
                Sexo = vm.Sexo,
                FechaNac = vm.FechaNacDateTime,
                FechaAlta = vm.FechaAlta,
                Foto = pic,
                EstablecimientoId = vm.EstablecimientoId
            };
        }

        [Authorize(Roles = "Administrador,Empleado,Paciente")]
        public async Task<ActionResult> DatosAdicionales(long? id, string email = "")
        {
            PacienteDto paciente;
            if (!string.IsNullOrEmpty(email))
            {
                paciente = await _pacienteServicio.GetByEmail(email);

                if (paciente == null) return RedirectToAction("Error", "Home");
            }
            else
            {
                if (id == null) return RedirectToAction("Error", "Home");

                paciente = await _pacienteServicio.GetById(id.Value);
            }

            return View(new PacienteViewModel()
            {
                Id = paciente.Id,
                Nombre = paciente.Nombre,
                Apellido = paciente.Apellido,
                TieneObservacion = paciente.TieneObservacion,
            });
        }

        public async Task<ActionResult> DatosAntropometricosParcial(long? id, bool eliminado = false)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _datoAntropometricoServicio.GetByIdPaciente(id.Value);

            var datosAntropometricos = Mapper.Map<IEnumerable<DatoAntropometricoViewModel>>(datos);

            ViewBag.Eliminado = eliminado;
            ViewBag.PacienteId = id;

            return PartialView(datosAntropometricos.Where(x => x.Eliminado == eliminado).ToList());

        }

        public async Task<ActionResult> ObservacionesParcial(long? id, bool? reload)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _observacionServicio.GetByPacienteId(id.Value);

            var observacion = Mapper.Map<ObservacionViewModel>(dato);

            ViewBag.PacienteId = id;

            if(reload == null)
            {
                return PartialView(observacion);
            }
            else
            {
                var json = new { vista = RenderRazorViewToString("~/Views/Paciente/ObservacionesParcial.cshtml", observacion), reload = reload };

                return Json(json, JsonRequestBehavior.AllowGet);
            }

            

        }

        public async Task<ActionResult> PlanesAlimenticiosParcial(long? id, bool eliminado = false)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _planAlimenticioServicio.GetByIdPaciente(id.Value);

            var planesAlimenticios = Mapper.Map<IEnumerable<PlanAlimenticioViewModel>>(datos);

            ViewBag.Eliminado = eliminado;
            ViewBag.PacienteId = id;

            return PartialView(planesAlimenticios.Where(x=> x.Eliminado == eliminado).ToList());
        }

        public async Task<ActionResult> DatosAnaliticosParcial(long? id, bool eliminado = false)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _datoAnaliticoServicio.GetByIdPaciente(id.Value);

            var datosAnaliticos = Mapper.Map<IEnumerable<DatoAnaliticoViewModel>>(datos);

            ViewBag.Eliminado = eliminado;
            ViewBag.PacienteId = id;

            return PartialView(datosAnaliticos.Where(x => x.Eliminado == eliminado).ToList());
        }

        public async Task<ActionResult> TurnosParcial(long? id, bool eliminado = false)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _turnoServicio.GetByIdPaciente(id.Value);

            var turnos = Mapper.Map<IEnumerable<TurnoViewModel>>(datos);

            ViewBag.Eliminado = eliminado;
            ViewBag.PacienteId = id;

            return PartialView(turnos.Where(x => x.Eliminado == eliminado).ToList());
        }

        public async Task<ActionResult> ObjetivosParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _objetivoServicio.GetByPacienteId(id.Value);

            var objetivo = new ObjetivoViewModel()
            {
                Descripcion = datos.Descripcion,
                Id = datos.Id,
                PacienteId = datos.PacienteId
            };

            return PartialView(objetivo);
        }

        public async Task<ActionResult> AnamnesisParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _anamnesisServicio.GetByPacienteId(id.Value);

            var anamnesis = new AnamnesisViewModel()
            {
                Descripcion = datos.Descripcion,
                Id = datos.Id,
                PacienteId = datos.PacienteId
            };

            return PartialView(anamnesis);
        }

        public async Task<ActionResult> EstrategiaParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var datos = await _estrategiaServicio.GetByPacienteId(id.Value);

            var estrategia = new EstrategiaViewModel()
            {
                Descripcion = datos.Descripcion,
                Id = datos.Id,
                PacienteId = datos.PacienteId
            };

            return PartialView(estrategia);
        }

        public async Task<ActionResult> ObservacionesPatologiasParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var dato = await _observacionServicio.GetByPacienteId(id.Value);

            var observacion = Mapper.Map<ObservacionViewModel>(dato);

            ViewBag.PacienteId = id;

            return PartialView(observacion);
        }


        //=======================Metodos privados ======================

        private static PacienteABMViewModel ModificarFechas(PacienteABMViewModel vm)
        {
            var horaEntrada = vm.FechaNac.ToString().Split('/');

            vm.FechaNacDateTime = DateTime.Parse($"{horaEntrada[1]}/{horaEntrada[0]}/{horaEntrada[2]}");

            return vm;
        }

        protected string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
