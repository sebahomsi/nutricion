using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Anamnesis;
using Servicio.Interface.Anamnesis;

namespace NutricionWeb.Controllers.Anamnesis
{
    public class AnamnesisController : Controller
    {
        private readonly IAnamnesisServicio _anamnesisServicio;

        public AnamnesisController(IAnamnesisServicio anamnesisServicio)
        {
            _anamnesisServicio = anamnesisServicio;
        }

        // GET: Anamnesis
        public ActionResult Index()
        {
            return View();
        }

        // GET: Anamnesis/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Anamnesis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anamnesis/Create
        [HttpPost]
        public async Task<ActionResult> Create(AnamnesisViewModel vm)
        {
            try
            {
                var anamnesis = await _anamnesisServicio.GetByPacienteId(vm.PacienteId);
                var idReal = anamnesis.Id;
                if (idReal == 0)
                {
                    // nuevo
                    var dto = new AnamnesisDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId
                    };

                    await _anamnesisServicio.Add(dto);
                }
                else
                {
                    // modificar
                    var dto = new AnamnesisDto()
                    {
                        Descripcion = vm.Descripcion,
                        PacienteId = vm.PacienteId,
                        Id = idReal
                    };

                    await _anamnesisServicio.Update(dto);
                }

                //return RedirectToAction("Index");
                return RedirectToAction("DatosAdicionales", "Paciente", new { Id = vm.PacienteId });
            }
            catch(Exception e)
            {
                return null;
            }
        }

        // GET: Anamnesis/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Anamnesis/Edit/5
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

        // GET: Anamnesis/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Anamnesis/Delete/5
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
