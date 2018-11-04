using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Paciente;
using PagedList;
using Servicio.Interface.Paciente;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Paciente
{
    public class PacienteController : Controller
    {
        private readonly IPacienteServicio _pacienteServicio; //llaman e inicializan abajo para poder usar los servicios en el controlador

        public PacienteController(IPacienteServicio pacienteServicio)
        {
            _pacienteServicio = pacienteServicio;
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
                TieneAnalitico = x.TieneAnalitico
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }


        // GET: Paciente/Create
        public async Task<ActionResult> Create()
        {
            return View(new PacienteViewModel());
        }

        // POST: Paciente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paciente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Paciente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paciente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Paciente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Paciente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}
