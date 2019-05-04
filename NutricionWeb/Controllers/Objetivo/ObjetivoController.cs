using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Objetivo;
using Servicio.Interface.Objetivo;

namespace NutricionWeb.Controllers.Objetivo
{
    public class ObjetivoController : Controller
    {
        private readonly IObjetivoServicio _objetivoServicio;

        public ObjetivoController(IObjetivoServicio objetivoServicio)
        {
            _objetivoServicio = objetivoServicio;
        }

        // GET: Objetivo
        public ActionResult Index()
        {
            return View();
        }

        // GET: Objetivo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Objetivo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Objetivo/Create
        [HttpPost]
        public async Task<ActionResult> Create(ObjetivoViewModel vm)
        {
            try
            {
                if (vm.Id == 0)
                {
                    // nuevo
                    var dto = new ObjetivoDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _objetivoServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new ObjetivoDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = vm.Id
                    };

                    await _objetivoServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new {Id = vm.PacienteId});
            }
            catch (Exception e)
            {
                return null;
            }
        }

        // GET: Objetivo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Objetivo/Edit/5
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

        // GET: Objetivo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Objetivo/Delete/5
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
