using AutoMapper;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Dia;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace NutricionWeb.Controllers.Comida
{
    [Authorize(Roles = "Administrador")]
    public class ComidaController : Controller
    {
        private readonly IComidaServicio _comidaServicio;
        private readonly IDiaServicio _diaServicio;
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IComidaDetalleServicio _comidaDetalleServicio;

        public ComidaController(IComidaServicio comidaServicio,
            IDiaServicio diaServicio,
            IPlanAlimenticioServicio planAlimenticioServicio,
            IComidaDetalleServicio comidaDetalleServicio)
        {
            _comidaServicio = comidaServicio;
            _diaServicio = diaServicio;
            _planAlimenticioServicio = planAlimenticioServicio;
            _comidaDetalleServicio = comidaDetalleServicio;
        }
        // GET: Comida
        public ActionResult Index()
        {
            return View();
        }

        // GET: Comida/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var comida = await _comidaServicio.GetById(id.Value);

            return View(new ComidaViewModel()
            {
                Id = comida.Id,
                Codigo = comida.Codigo,
                Descripcion = comida.Descripcion,
                DiaId = comida.DiaId,
                DiaStr = comida.DiaStr,
                ComidasDetalles = comida.ComidasDetalles.Select(x => new ComidaDetalleViewModel()
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

        /// <summary>
        /// PONER EN UN SERVICIO (DIASERVICIO)
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="comidaId"></param>
        /// <returns></returns>
        public async Task<ActionResult> DuplicarComida(long planId, long comidaId)
        {
            var comida = await _comidaServicio.GetById(comidaId);
            var plan = await _planAlimenticioServicio.GetById(planId);

            TempData["ComidaId"] = comidaId;

            TempData["PlanId"] = planId;

            TempData["DiaCopiarId"] = comida.DiaId;

            List<DiaViewModel> dias = new List<DiaViewModel>();

            foreach (var dia in plan.Dias)
            {
                foreach (var com in dia.Comidas)
                {
                    if (com.Descripcion == comida.Descripcion)
                    {
                        if (com.ComidasDetalles.Count > 0)
                        {
                            dias.Add(Mapper.Map<DiaViewModel>(dia));
                        }
                    }
                }
            }

            return PartialView(dias);
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarComida(long diaId)
        {
            var diaVacioId = TempData["DiaCopiarId"];
            var comidaCopiarId = (long)TempData["ComidaId"];
            var planId = (long)TempData["PlanId"];
            var comdidaDto = new ComidaDetalleDto();


            var plan = await _planAlimenticioServicio.GetById(planId);
            var comidaTarget = await _comidaServicio.GetById(comidaCopiarId);

            foreach (var dia in plan.Dias)
            {
                if (dia.Id == diaId)
                {
                    foreach (var comida in dia.Comidas)
                    {
                        if (comida.Descripcion == comidaTarget.Descripcion)
                        {
                            foreach (var detalle in comida.ComidasDetalles)
                            {
                                comdidaDto = new ComidaDetalleDto()
                                {
                                    ComidaId = comidaCopiarId,
                                    Comentario = detalle.Comentario,
                                    OpcionId = detalle.OpcionId,
                                    Eliminado = false,
                                };
                                comdidaDto.Codigo = await _comidaDetalleServicio.GetNextCode();
                                await _comidaDetalleServicio.Add(comdidaDto);
                            }
                        }
                    }
                }
            }

           


            return RedirectToAction("ExportarPlanOrdenado", "PlanAlimenticio", new { id = TempData["PlanId"] });
        }

        public async Task<ActionResult> DetalleComida(long comidaId)
        {
            var comida = await _comidaServicio.GetById(comidaId);

            var model = new ComidaViewModel()
            {
            };

            return PartialView(model);
        }
    }
}
