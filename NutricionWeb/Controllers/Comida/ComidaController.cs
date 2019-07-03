using AutoMapper;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Dia;
using NutricionWeb.Models.Opcion;
using NutricionWeb.Models.OpcionDetalle;
using Servicio.Interface.Comida;
using Servicio.Interface.ComidaDetalle;
using Servicio.Interface.Dia;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // GET: Comida/Details/5
        [Authorize(Roles = "Administrador, Empleado")]
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

        // POST: Comida/Delete/5
        [HttpPost]
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> Delete(long detalleId, long? planId)
        {
            try
            {
                 await _comidaDetalleServicio.Delete(detalleId);
                 await _planAlimenticioServicio.CalculateTotalCalories(planId.Value);
            }
            catch (Exception ex)
            {
                return Json(new { estado = false, mensaje = ex.Message });
            }

            return Json(new { estado = true });
        }

        /// <summary>
        /// PONER EN UN SERVICIO (DIASERVICIO)
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="comidaId"></param>
        /// <returns></returns>
         [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
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
        [Authorize(Roles = "Administrador, Empleado")]
        public async Task<ActionResult> DetalleComida(long detalleId , long? planId)
        {
            ViewBag.PlanId = planId.Value;

            var detalle = await _comidaDetalleServicio.GetById(detalleId);

            var model = new ComidaDetalleViewModel()
            {
                    Id = detalle.Id,
                    Codigo = detalle.Codigo,
                    Eliminado = detalle.Eliminado,
                    Comentario = detalle.Comentario,
                    ComidaId = detalle.ComidaId,
                    OpcionId = detalle.OpcionId,
                    ComidaStr = detalle.ComidaStr,
                    OpcionStr = detalle.OpcionStr,
                    Opcion = new OpcionViewModel()
                    {
                        Id = detalle.Opcion.Id,
                        Codigo = detalle.Opcion.Codigo,
                        Descripcion = detalle.Opcion.Descripcion,
                        Eliminado = detalle.Opcion.Eliminado,
                        OpcionDetalles = detalle.Opcion.OpcionDetalles.Where(x=> x.Eliminado == false).Select(o=> new OpcionDetalleViewModel()
                        {
                            Id = o.Id,
                            Codigo = o.Codigo,
                            Eliminado = o.Eliminado,
                            OpcionId = o.OpcionId,
                            OpcionStr = o.OpcionStr,
                            AlimentoId = o.AlimentoId,
                            AlimentoStr = o.AlimentoStr,
                            Cantidad = o.Cantidad,
                            UnidadMedidaId = o.UnidadMedidaId,
                            UnidadMedidaStr = o.UnidadMedidaStr
                        }).ToList()
                    }
               
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> EditarComentario(long detalleId , string comentario)
        {
            try
            {
                var detalle = await _comidaDetalleServicio.GetById(detalleId);

                detalle.Comentario = comentario;

                await _comidaDetalleServicio.Update(detalle);              
            }
            catch (Exception ex)
            {
                return Json(new { estado = false, mensaje = ex.Message });
            }

            return Json(new { estado = true });
        }

        public async Task<ActionResult> DuplicarComidaDeOtroPlan(long? planDesdeId, long? planHastaId, string comidaDescripcion)
        {
            try
            {
                await _planAlimenticioServicio.DuplicarComidaDeOtroPlan(planDesdeId, planHastaId, comidaDescripcion);
                await _planAlimenticioServicio.CalculateTotalCalories(planHastaId.Value);
            }
            catch (Exception ex)
            {
                return Json(new { estado = false, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { estado = true }, JsonRequestBehavior.AllowGet);
        }
    }
}
