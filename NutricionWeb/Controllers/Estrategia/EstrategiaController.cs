using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Estrategia;
using Servicio.Interface.Estrategia;

namespace NutricionWeb.Controllers.Estrategia
{
    public class EstrategiaController : Controller
    {

        private readonly IEstrategiaServicio _estrategiaServicio;

        public EstrategiaController(IEstrategiaServicio estrategiaServicio)
        {
            _estrategiaServicio = estrategiaServicio;
        }

        // GET: Estrategia
        public ActionResult Index()
        {
            return View();
        }

        // GET: Estrategia/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Estrategia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estrategia/Create
        [HttpPost]
        public async Task<ActionResult> Create(EstrategiaViewModel vm)
        {
            try
            {
                var estrategia = await _estrategiaServicio.GetByPacienteId(vm.PacienteId);
                var idReal = estrategia.Id;
                if (idReal == 0)
                {
                    // nuevo
                    var dto = new EstrategiaDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _estrategiaServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new EstrategiaDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = idReal
                    };

                    await _estrategiaServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new { Id = vm.PacienteId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Estrategia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estrategia/Edit/5
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

        // GET: Estrategia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Estrategia/Delete/5
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
    }
}
