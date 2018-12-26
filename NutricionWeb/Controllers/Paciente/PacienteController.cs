using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Helpers.Persona;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.Paciente;
using static NutricionWeb.Helpers.PagedList;
using static NutricionWeb.Helpers.File;

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
        public async Task<ActionResult> Index(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1; //declaramos que pageNumber sea igual al valor de page, si page no tiene valor le asigna 1

            var pacientes =
                await _pacienteServicio.Get(!string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty); //se traen todos los pacientes, si la cadena es diferente de null se le pasa al metodo el valor de cadenaBuscar, sino le pasa string.Empty

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
                Estado = x.Estado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: Paciente/Create
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
                Estado = paciente.Estado,
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
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Paciente/Delete/5
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
                Estado = paciente.Estado,
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
        public async Task<ActionResult> Details(long? id)
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
                Mail = paciente.Mail,
                FotoStr = paciente.Foto,
                Eliminado = paciente.Eliminado,
                Estado = paciente.Estado,
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
    }
}
