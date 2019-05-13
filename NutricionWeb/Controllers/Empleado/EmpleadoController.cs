using NutricionWeb.Helpers.Persona;
using NutricionWeb.Models.Empleado;
using PagedList;
using Servicio.Interface.Empleado;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Helpers.Establecimiento;
using static NutricionWeb.Helpers.File;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Empleado
{
    [Authorize(Roles = "Administrador")]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoServicio _empleadoServicio;
        private readonly IComboBoxSexo _comboBoxSexo;
        private readonly IComboBoxEstablecimiento _comboBoxEstablecimiento;

        public EmpleadoController(IEmpleadoServicio empleadoServicio, 
            IComboBoxSexo comboBoxSexo,
            IComboBoxEstablecimiento comboBoxEstablecimiento)
        {
            _empleadoServicio = empleadoServicio;
            _comboBoxSexo = comboBoxSexo;
            _comboBoxEstablecimiento = comboBoxEstablecimiento;
        }

        // GET: Empleado
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;

            ViewBag.Eliminado = eliminado;

            var empleados =
                await _empleadoServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);
            if (empleados == null) return HttpNotFound();

            return View(empleados.Select(x => new EmpleadoViewModel()
            {
                Id = x.Id,
                Legajo = x.Legajo,
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



        // GET: Empleado/Create
        public async Task<ActionResult> Create()
        {
            return View(new EmpleadoABMViewModel()
            {
                Sexos = await _comboBoxSexo.Poblar(),
                Establecimientos = await _comboBoxEstablecimiento.Poblar()
            });
        }

        // POST: Paciente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmpleadoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    var empleadoDto = CargarDatos(vm, pic);

                    empleadoDto.Legajo = await _empleadoServicio.GetNextCode();

                    await _empleadoServicio.Add(empleadoDto);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                vm.Sexos = await _comboBoxSexo.Poblar();
                vm.Establecimientos = await _comboBoxEstablecimiento.Poblar();
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // GET: Empleado/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var empleado = await _empleadoServicio.GetById(id.Value);

            return View(new EmpleadoABMViewModel()
            {
                Sexos = await _comboBoxSexo.Poblar(),
                Id = empleado.Id,
                Legajo = empleado.Legajo,
                Apellido = empleado.Apellido,
                Nombre = empleado.Nombre,
                Celular = empleado.Celular,
                Telefono = empleado.Telefono,
                Direccion = empleado.Direccion,
                Dni = empleado.Dni,
                FechaNac = empleado.FechaNac,
                Sexo = empleado.Sexo,
                Mail = empleado.Mail,
                Eliminado = empleado.Eliminado,
            });
        }

        // POST: Empleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EmpleadoABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var pic = string.Empty;
                    pic = vm.Foto != null ? Upload(vm.Foto, FolderDefault) : "~/Content/Imagenes/user-icon.jpg";

                    var empleadoDto = CargarDatos(vm, pic);

                    await _empleadoServicio.Update(empleadoDto);

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

        // GET: Empleado/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var empleado = await _empleadoServicio.GetById(id.Value);

            return View(new EmpleadoViewModel()
            {
                Id = empleado.Id,
                Legajo = empleado.Legajo,
                Apellido = empleado.Apellido,
                Nombre = empleado.Nombre,
                Celular = empleado.Celular,
                Telefono = empleado.Telefono,
                Direccion = empleado.Direccion,
                Dni = empleado.Dni,
                FechaNac = empleado.FechaNac,
                Sexo = empleado.Sexo,
                Mail = empleado.Mail,
                FotoStr = empleado.Foto,
                Eliminado = empleado.Eliminado,
            });
        }

        // POST: Empleado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EmpleadoViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _empleadoServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            return RedirectToAction("Index");
        }
        // GET: Empleado/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var empleado = await _empleadoServicio.GetById(id.Value);

            return View(new EmpleadoViewModel()
            {
                Id = empleado.Id,
                Legajo = empleado.Legajo,
                Apellido = empleado.Apellido,
                Nombre = empleado.Nombre,
                Celular = empleado.Celular,
                Telefono = empleado.Telefono,
                Direccion = empleado.Direccion,
                Dni = empleado.Dni,
                FechaNac = empleado.FechaNac,
                Sexo = empleado.Sexo,
                Mail = empleado.Mail,
                FotoStr = empleado.Foto,
                Eliminado = empleado.Eliminado,
            });
        }

        //===============================================================================//
        private EmpleadoDto CargarDatos(EmpleadoABMViewModel vm, string pic)
        {
            return new EmpleadoDto()
            {
                Id = vm.Id,
                Legajo = vm.Legajo,
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Mail = vm.Mail,
                Dni = vm.Dni,
                Celular = vm.Celular,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion,
                Sexo = vm.Sexo,
                FechaNac = vm.FechaNac,
                EstablecimientoId = vm.EstablecimientoId,
                Foto = pic
            };
        }
    }
}
