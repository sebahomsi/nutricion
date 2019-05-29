using NutricionWeb.Models.AlergiaIntolerancia;
using NutricionWeb.Models.Alimento;
using NutricionWeb.Models.Observacion;
using NutricionWeb.Models.Patologia;
using PagedList;
using Servicio.Interface.Observacion;
using Servicio.Interface.Paciente;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
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
                await _observacionServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            return View(observaciones.Select(x => new ObservacionViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Tabaco = x.Tabaco,
                Alcohol = x.Alcohol,
                ActividadFisica = x.ActividadFisica,
                AntecedentesFamiliares = x.AntecedentesFamiliares,
                RitmoEvacuatorio = x.RitmoEvacuatorio,
                HorasSuenio = x.HorasSuenio,
                Medicacion = x.Medicacion,
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

                    await _observacionServicio.Add(observacionDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> CreateParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new ObservacionABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
            });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(ObservacionABMViewModel vm)
        {
            bool reload = false;
            try
            {
                if (ModelState.IsValid)
                {
                    var datosDto = CargarDatos(vm);
                    datosDto.Codigo = await _observacionServicio.GetNextCode();

                    //await _observacionServicio.Add(datosDto);
                    reload = true;
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
            return RedirectToAction("ObservacionesParcial", "Paciente", new { id = vm.PacienteId, reload = reload });

        }

        // GET
        public async Task<ActionResult> EditParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var observacion = await _observacionServicio.GetById(id.Value);

            return PartialView(new ObservacionABMViewModel()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.PacienteStr,
                Tabaco = observacion.Tabaco,
                Alcohol = observacion.Alcohol,
                ActividadFisica = observacion.ActividadFisica,
                AntecedentesFamiliares = observacion.AntecedentesFamiliares,
                Medicacion = observacion.Medicacion,
                RitmoEvacuatorio = observacion.RitmoEvacuatorio,
                HorasSuenio = observacion.HorasSuenio,
                Eliminado = observacion.Eliminado
            });
        }

        // POST: Observacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditParcial(ObservacionABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var observacionDto = CargarDatos(vm);

                    await _observacionServicio.Update(observacionDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("ObservacionesParcial", "Paciente", new { id = vm.PacienteId });

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
                Tabaco = observacion.Tabaco,
                Alcohol = observacion.Alcohol,
                ActividadFisica = observacion.ActividadFisica,
                AntecedentesFamiliares = observacion.AntecedentesFamiliares,
                Medicacion = observacion.Medicacion,
                RitmoEvacuatorio = observacion.RitmoEvacuatorio,
                HorasSuenio = observacion.HorasSuenio,
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
            catch (Exception ex)
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
                Tabaco = observacion.Tabaco,
                Alcohol = observacion.Alcohol,
                ActividadFisica = observacion.ActividadFisica,
                AntecedentesFamiliares = observacion.AntecedentesFamiliares,
                Medicacion = observacion.Medicacion,
                RitmoEvacuatorio = observacion.RitmoEvacuatorio,
                HorasSuenio = observacion.HorasSuenio,
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

            var observacion = await _observacionServicio.GetById(id.Value);

            return View(new ObservacionViewModel()
            {
                Id = observacion.Id,
                Codigo = observacion.Codigo,
                PacienteId = observacion.PacienteId,
                PacienteStr = observacion.PacienteStr,
                Tabaco = observacion.Tabaco,
                Alcohol = observacion.Alcohol,
                ActividadFisica = observacion.ActividadFisica,
                AntecedentesFamiliares = observacion.AntecedentesFamiliares,
                Medicacion = observacion.Medicacion,
                RitmoEvacuatorio = observacion.RitmoEvacuatorio,
                HorasSuenio = observacion.HorasSuenio,
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
                Tabaco = vm.Tabaco,
                ActividadFisica = vm.ActividadFisica,
                AntecedentesFamiliares = vm.AntecedentesFamiliares,
                Alcohol = vm.Alcohol,
                Medicacion = vm.Medicacion,
                RitmoEvacuatorio = vm.RitmoEvacuatorio,
                HorasSuenio = vm.HorasSuenio,
                Eliminado = vm.Eliminado
            };
        }
    }
}
