using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Opcion;
using Servicio.Interface.Comida;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;

namespace NutricionWeb.Controllers.Comida
{
    [Authorize(Roles = "Administrador")]
    public class ComidaController : Controller
    {
        private readonly IComidaServicio _comidaServicio;
        private readonly IDiaServicio _diaServicio;
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;

        public ComidaController(IComidaServicio comidaServicio, IDiaServicio diaServicio, IPlanAlimenticioServicio planAlimenticioServicio)
        {
            _comidaServicio = comidaServicio;
            _diaServicio = diaServicio;
            _planAlimenticioServicio = planAlimenticioServicio;
        }
        // GET: Comida
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comida/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var comida = await _comidaServicio.GetById(id.Value);

            return View(new ComidaViewModel()
            {
                Id = comida.Id,
                Codigo = comida.Codigo,
                Descripcion = comida.Descripcion,
                DiaId = comida.DiaId,
                DiaStr = comida.DiaStr,
                ComidasDetalles = comida.ComidasDetalles.Select(x=> new ComidaDetalleViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Comentario = x.Comentario,
                    ComidaId = x.ComidaId,
                    ComidaStr = x.ComidaStr,
                    OpcionId = x.OpcionId,
                    OpcionStr = x.OpcionStr,
                    Eliminado = x.Eliminado,
                    
                }).ToList()

            });
        }

        // GET: Comida/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comida/Create
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

        // GET: Comida/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Comida/Edit/5
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

        // GET: Comida/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Comida/Delete/5
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
