using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.Dia;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;

namespace NutricionWeb.Controllers.Dia
{
    public class DiaController : Controller
    {
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IDiaServicio _diaServicio;

        public DiaController(IPlanAlimenticioServicio planAlimenticioServicio, IDiaServicio diaServicio)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _diaServicio = diaServicio;
        }
        // GET: Dia
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dia/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var dia = await _diaServicio.GetById(id.Value);

            return View(new DiaViewModel()
            {
                Id = dia.Id,
                Codigo = dia.Codigo,
                Descripcion = dia.Descripcion,
                PlanAlimenticioId = dia.PlanAlimenticioId,
                PlanAlimenticioStr = dia.PlanAlimenticioStr,
                Comidas = dia.Comidas.Select(x=> new ComidaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    DiaId = x.DiaId,
                    DiaStr = x.DiaStr
                }).ToList()
            });
        }

        // GET: Dia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dia/Create
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

        // GET: Dia/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dia/Edit/5
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

        // GET: Dia/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dia/Delete/5
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
