using NutricionWeb.Helpers.Persona;
using NutricionWeb.Models.DatoAnalitico;
using NutricionWeb.Models.DatoAntropometrico;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using NutricionWeb.Models.Turno;
using PagedList;
using Servicio.Interface.Paciente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using static NutricionWeb.Helpers.File;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Paciente
{

    public class PacienteController : Controller
    {
        private readonly IPacienteServicio _pacienteServicio; //llaman e inicializan abajo para poder usar los servicios en el controlador
        private readonly IComboBoxSexo _comboBoxSexo;

        public PacienteController(IPacienteServicio pacienteServicio, IComboBoxSexo comboBoxSexo)
        {
            _pacienteServicio = pacienteServicio;
            _comboBoxSexo = comboBoxSexo;
        }

        // GET: Paciente
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1; //declaramos que pageNumber sea igual al valor de page, si page no tiene valor le asigna 1

            ViewBag.Eliminado = eliminado;
            var pacientes =
                await _pacienteServicio.Get(eliminado,!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty); //se traen todos los pacientes, si la cadena es diferente de null se le pasa al metodo el valor de cadenaBuscar, sino le pasa string.Empty

            if (pacientes == null) return HttpNotFound(); //si pacientes no tiene nada retorna una pagina de no se encontró una poronga

            return View(pacientes.Select(x => new PacienteViewModel() //acá se cargan los campos que se van a ver en el listado del index y los que se van a usar para hacer las acciones, formateen la grilla una vez creada la vista
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


        // GET: Paciente/Create
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Create()
        {
            return View(new PacienteABMViewModel()
            {
                Sexos = await _comboBoxSexo.Poblar()
            });
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PacienteABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    var pacienteDto = CargarDatos(vm, pic);

                    pacienteDto.Codigo = await _pacienteServicio.GetNextCode();

                    await _pacienteServicio.Add(pacienteDto);
                }
                else
                {
                    var modelErrors = new List<string>();
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
                vm.Sexos = await _comboBoxSexo.Poblar();
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Edit/5
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
                Direccion = paciente.Direccion,
                Dni = paciente.Dni,
                FechaNac = paciente.FechaNac,
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
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    var pacienteDto = CargarDatos(vm, pic);

                    await _pacienteServicio.Update(pacienteDto);
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                vm.Sexos = await _comboBoxSexo.Poblar();
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return View(new PacienteViewModel()
            {
                Id = paciente.Id,
                Codigo = paciente.Codigo,
                Apellido = paciente.Apellido,
                Nombre = paciente.Nombre,
                Celular = paciente.Celular,
                Telefono = paciente.Telefono,
                Direccion = paciente.Direccion,
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
        public async Task<ActionResult> Details(long? id, string email="")
        {
            PacienteDto paciente;
            if (!string.IsNullOrEmpty(email))
            {
               paciente = await _pacienteServicio.GetByEmail(email);

               if (paciente == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"No se encontró el perfil");
            }
            else
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

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
                Direccion = paciente.Direccion,
                Dni = paciente.Dni,
                FechaNac = paciente.FechaNac.Date,
                Sexo = paciente.Sexo,
                Mail = paciente.Mail,
                FotoStr = paciente.Foto,
                Eliminado = paciente.Eliminado,
                TieneObservacion = paciente.TieneObservacion,
                //DatosAntropometricos = paciente.DatosAntropometricos.Select(p => new DatoAntropometricoViewModel()
                //{
                //    Id = p.Id,
                //    Codigo = p.Codigo,
                //    PacienteId = p.PacienteId,
                //    PacienteStr = p.PacienteStr,
                //    Altura = p.Altura,
                //    FechaMedicion = p.FechaMedicion,
                //    MasaGrasa = p.MasaGrasa,
                //    MasaCorporal = p.MasaCorporal,
                //    Peso = p.Peso,
                //    PerimetroCintura = p.PerimetroCintura,
                //    PerimetroCadera = p.PerimetroCadera,
                //    Eliminado = p.Eliminado
                //}).ToList(),
                //PlanesAlimenticios = paciente.PlanesAlimenticios.OrderBy(q => q.Fecha).Select(q => new PlanAlimenticioViewModel()
                //{
                //    Id = q.Id,
                //    Codigo = q.Codigo,
                //    Motivo = q.Motivo,
                //    Fecha = q.Fecha,
                //    PacienteId = q.PacienteId,
                //    PacienteStr = q.PacienteStr,
                //    Eliminado = q.Eliminado
                //}).ToList(),
                //Turnos = paciente.Turnos.OrderBy(t => t.HorarioEntrada).Select(t => new TurnoViewModel()
                //{
                //    Id = t.Id,
                //    Numero = t.Numero,
                //    Motivo = t.Motivo,
                //    PacienteId = t.PacienteId,
                //    PacienteStr = t.PacienteStr,
                //    HorarioEntrada = t.HorarioEntrada,
                //    HorarioSalida = t.HorarioSalida,
                //    Eliminado = t.Eliminado
                //}).ToList(),
                //DatosAnaliticos = paciente.DatosAnaliticos.OrderBy(x => x.Codigo).Select(x => new DatoAnaliticoViewModel()
                //{
                //    Id = x.Id,
                //    Codigo = x.Codigo,
                //    ColesterolHdl = x.ColesterolHdl,
                //    ColesterolLdl = x.ColesterolLdl,
                //    ColesterolTotal = x.ColesterolTotal,
                //    PresionDiastolica = x.PresionDiastolica,
                //    PresionSistolica = x.PresionSistolica,
                //    Trigliceridos = x.Trigliceridos,
                //    PacienteId = x.PacienteId,
                //    PacienteStr = x.PacienteStr,
                //    FechaMedicion = x.FechaMedicion,
                //    Eliminado = x.Eliminado
                //}).ToList()
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
                Direccion = vm.Direccion,
                Sexo = vm.Sexo,
                FechaNac = vm.FechaNac,
                Foto = pic 
            };
        }

        [Authorize(Roles = "Administrador, Empleado, Paciente")]
        public async Task<ActionResult> DatosAdicionales(long? id, string email = "")
        {
            PacienteDto paciente;
            if (!string.IsNullOrEmpty(email))
            {
                paciente = await _pacienteServicio.GetByEmail(email);

                if (paciente == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No se encontró el perfil");
            }
            else
            {
                if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                paciente = await _pacienteServicio.GetById(id.Value);
            }

            return View(new PacienteViewModel()
            {
                Id = paciente.Id,
            });
        }

        public async Task<ActionResult> DatosAntropometricosParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(paciente.DatosAntropometricos.Select(x=> new DatoAntropometricoViewModel()
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

            }).ToList());
        
        }

        public async Task<ActionResult> PlanesAlimenticiosParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(paciente.PlanesAlimenticios.Select(x => new PlanAlimenticioABMViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Motivo = x.Motivo,
                Fecha = x.Fecha,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Eliminado = x.Eliminado

            }).ToList());
        }

        public async Task<ActionResult> DatosAnaliticosParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new PacienteViewModel()
            {
                DatosAnaliticos = paciente.DatosAnaliticos
                    .Select(x => new DatoAnaliticoViewModel()
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        ColesterolHdl = x.ColesterolHdl,
                        ColesterolLdl = x.ColesterolLdl,
                        ColesterolTotal = x.ColesterolTotal,
                        PresionDiastolica = x.PresionDiastolica,
                        PresionSistolica = x.PresionSistolica,
                        Trigliceridos = x.Trigliceridos,
                        PacienteId = x.PacienteId,
                        PacienteStr = x.PacienteStr,
                        FechaMedicion = x.FechaMedicion,
                        Eliminado = x.Eliminado
                    }).ToList()
            });
        }

        public async Task<ActionResult> TurnosParcial(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new PacienteViewModel()
            {
                Turnos = paciente.Turnos
                    .Select(x => new TurnoViewModel()
                    {
                        Id = x.Id,
                        Numero = x.Numero,
                        Motivo = x.Motivo,
                        PacienteId = x.PacienteId,
                        PacienteStr = x.PacienteStr,
                        HorarioEntrada = x.HorarioEntrada,
                        HorarioSalida = x.HorarioSalida,
                        Eliminado = x.Eliminado
                    }).ToList()
            });
        }
    }
}
