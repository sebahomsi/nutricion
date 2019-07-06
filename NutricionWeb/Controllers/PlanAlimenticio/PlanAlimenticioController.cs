using AutoMapper;
using NutricionWeb.Models.Comida;
using NutricionWeb.Models.ComidaDetalle;
using NutricionWeb.Models.Dia;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.PlanAlimenticio;
using PagedList;
using Rotativa;
using Servicio.Interface.Alimento;
using Servicio.Interface.Dia;
using Servicio.Interface.Opcion;
using Servicio.Interface.Paciente;
using Servicio.Interface.PlanAlimenticio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using NutricionWeb.Helpers.SubGrupo;
using NutricionWeb.Models.Opcion;
using Servicio.Alimento;
using Servicio.Comida;
using Servicio.ComidaDetalle;
using Servicio.Dia;
using Servicio.Opcion;
using Servicio.Paciente;
using Servicio.PlanAlimenticio;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.PlanAlimenticio
{
    public class PlanAlimenticioController : ControllerBase
    {
        private readonly IPlanAlimenticioServicio _planAlimenticioServicio;
        private readonly IPacienteServicio _pacienteServicio;
        private readonly IDiaServicio _diaServicio;
        private readonly IComboBoxSubGrupo _comboBoxSubGrupo;

        public PlanAlimenticioController(IPlanAlimenticioServicio planAlimenticioServicio, IPacienteServicio pacienteServicio, IDiaServicio diaServicio, IComboBoxSubGrupo comboBoxSubGrupo)
        {
            _planAlimenticioServicio = planAlimenticioServicio;
            _pacienteServicio = pacienteServicio;
            _diaServicio = diaServicio;
            _comboBoxSubGrupo = comboBoxSubGrupo;
        }

        //public async Task<Byte[]> GeneratePdfMail(long planId)
        //{
        //    var plan = await _planAlimenticioServicio.GetById(planId);

        //    var mailpdft = new ActionAsPdf(Url.Action("GeneratePdf", "PlanAlimenticio"), new { id = planId })
        //    {
        //        FileName = "PlanAlimenticio_" + plan.PacienteStr + ".pdf",
        //        PageSize = Rotativa.Options.Size.A4,
        //        PageOrientation = Rotativa.Options.Orientation.Landscape,
        //    };

        //    Byte[] PdfData = mailpdft.BuildFile(ControllerContext);
        //    return PdfData;
        //}


        // GET: PlanAlimenticio
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index(int? page, string cadenaBuscar, bool eliminado = false)
        {
            var pageNumber = page ?? 1;
            ViewBag.FilterValue = cadenaBuscar;
            ViewBag.Eliminado = eliminado;

            var planes =
                await _planAlimenticioServicio.Get(eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (planes == null) return HttpNotFound();



            return View(planes.Select(x => new PlanAlimenticioViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Fecha = x.Fecha,
                Motivo = x.Motivo,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Comentarios = x.Comentarios,
                Eliminado = x.Eliminado,
                TotalCalorias = x.TotalCalorias
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));


        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DuplicarPlan(int? volver,long? planId)
        {
            ViewBag.Volver = volver ?? 0;
            ViewBag.PlanId = planId ?? 0;
            return View(new DuplicarViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> DuplicarPlan(DuplicarViewModel vm)
        {
            var planId = long.Parse(Request["planId"]);
            var volver = int.Parse(Request["volver"]);
            try
            {
                if (ModelState.IsValid)
                {
                    await _planAlimenticioServicio.DuplicatePlan(vm.PlanId, vm.PacienteId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }

            if (volver == 1)
            {
                return RedirectToAction("ExportarPlanOrdenado", new {id = planId});
            }
            return RedirectToAction("Index");
        }


        // GET: PlanAlimenticio/Create
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Create()
        {
            return View(new PlanAlimenticioABMViewModel());
        }

        // POST: PlanAlimenticio/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    planDto.Codigo = await _planAlimenticioServicio.GetNextCode();

                    var planId = await _planAlimenticioServicio.Add(planDto);
                    await _diaServicio.GenerarDias(planId);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> CreateParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(id.Value);

            return PartialView(new PlanAlimenticioABMViewModel()
            {
                PacienteId = paciente.Id,
                PacienteStr = $"{paciente.Apellido} {paciente.Nombre}"
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateParcial(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    planDto.Codigo = await _planAlimenticioServicio.GetNextCode();

                    var planId = await _planAlimenticioServicio.Add(planDto);
                    await _diaServicio.GenerarDias(planId);
                }
                else
                {
                    return PartialView(vm);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("PlanesAlimenticiosParcial", "Paciente", new { id = vm.PacienteId });

        }

        // GET: PlanAlimenticio/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioABMViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado
            });
        }

        // POST: PlanAlimenticio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    await _planAlimenticioServicio.Update(planDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> EditParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return PartialView(new PlanAlimenticioABMViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado
            });
        }

        // POST: PlanAlimenticio/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditParcial(PlanAlimenticioABMViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var planDto = CargarDatos(vm);
                    await _planAlimenticioServicio.Update(planDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("PlanesAlimenticiosParcial", "Paciente", new { id = vm.PacienteId });

        }

        // GET: PlanAlimenticio/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                TotalCalorias = plan.TotalCalorias
            });
        }

        // POST: PlanAlimenticio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(PlanAlimenticioViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _planAlimenticioServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DeleteParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return PartialView(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                TotalCalorias = plan.TotalCalorias
            });
        }

        // POST: PlanAlimenticio/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteParcial(PlanAlimenticioViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _planAlimenticioServicio.Delete(vm.Id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return PartialView(vm);
            }
            return RedirectToAction("PlanesAlimenticiosParcial", "Paciente", new { id = vm.PacienteId });
        }


        // GET: PlanAlimenticio/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return View(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                TotalCalorias = plan.TotalCalorias,
                Dias = plan.Dias.Select(x => new DiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    Comidas = x.Comidas.Select(q => new ComidaViewModel()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.DiaStr,
                        ComidasDetalles = q.ComidasDetalles.Select(t => new ComidaDetalleViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Comentario = t.Comentario,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            OpcionId = t.OpcionId,
                            OpcionStr = t.OpcionStr,
                            Eliminado = t.Eliminado,
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> DetailsParcial(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            return PartialView(new PlanAlimenticioViewModel()
            {
                Id = plan.Id,
                Codigo = plan.Codigo,
                Fecha = plan.Fecha,
                Motivo = plan.Motivo,
                PacienteId = plan.PacienteId,
                PacienteStr = plan.PacienteStr,
                Comentarios = plan.Comentarios,
                Eliminado = plan.Eliminado,
                TotalCalorias = plan.TotalCalorias,
                Dias = plan.Dias.Select(x => new DiaViewModel()
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion,
                    PlanAlimenticioId = x.PlanAlimenticioId,
                    Comidas = x.Comidas.Select(q => new ComidaViewModel()
                    {
                        Id = q.Id,
                        Codigo = q.Codigo,
                        Descripcion = q.Descripcion,
                        DiaId = q.DiaId,
                        DiaStr = q.DiaStr,
                        ComidasDetalles = q.ComidasDetalles.Select(t => new ComidaDetalleViewModel()
                        {
                            Id = t.Id,
                            Codigo = t.Codigo,
                            Comentario = t.Comentario,
                            ComidaId = t.ComidaId,
                            ComidaStr = t.ComidaStr,
                            OpcionId = t.OpcionId,
                            OpcionStr = t.OpcionStr,
                            Eliminado = t.Eliminado,
                        }).ToList()
                    }).ToList()
                }).ToList()
            });
        }

        //================================================================Metodos Especiales

        public async Task<ActionResult> ExportarPlanOrdenado(long? id)
        {
            if (id == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(id.Value);

            var comidas = await _planAlimenticioServicio.GetSortringComidas(id.Value);

            ViewBag.Cmb = await _comboBoxSubGrupo.Poblar();

            ViewBag.PlanId = id;
            ViewBag.Paciente = plan.PacienteStr;
            ViewBag.PacienteId = plan.PacienteId;
            ViewBag.Recetario = plan.Comentarios;
            ViewBag.Calorias = plan.TotalCalorias;
            if (plan.TotalCalorias == 0)
            {
                ViewBag.Carbos = 0;
                ViewBag.Protes = 0;
                ViewBag.Grasas = 0;

                ViewBag.CarboPorce = 0;
                ViewBag.GrasaPorce = 0;
                ViewBag.ProtePorce = 0;
            }
            else
            {
                ViewBag.Carbos = await _planAlimenticioServicio.CalculateTotalCaloriesCarbos(id.Value);
                ViewBag.Protes = await _planAlimenticioServicio.CalculateTotalCaloriesProtes(id.Value);
                ViewBag.Grasas = await _planAlimenticioServicio.CalculateTotalCaloriesGrasas(id.Value);

                ViewBag.CarboPorce = await _planAlimenticioServicio.CalculateTotalCaloriesCarbos(id.Value) * 100 / plan.TotalCalorias;
                ViewBag.GrasaPorce = await _planAlimenticioServicio.CalculateTotalCaloriesGrasas(id.Value) * 100 / plan.TotalCalorias;
                ViewBag.ProtePorce = await _planAlimenticioServicio.CalculateTotalCaloriesProtes(id.Value) * 100 / plan.TotalCalorias;
            }

          

            var comidasVm = Mapper.Map<PlanAlimenticioVistaViewModel>(comidas);

            return View(comidasVm);
        }

        public async Task<ActionResult> ExportarPlanPdf(long id)
        {
            var plan = await _planAlimenticioServicio.GetById(id);

            var comidas = await _planAlimenticioServicio.GetSortringComidas(id);

            ViewBag.PlanId = id;

            ViewBag.Calorias = plan.TotalCalorias;

            var comidasVm = Mapper.Map<PlanAlimenticioVistaViewModel>(comidas);

            return View(comidasVm);
        }

        public async Task<ActionResult> GeneratePdf(long planId)
        {
            var plan = await _planAlimenticioServicio.GetById(planId);

            return new ActionAsPdf("ExportarPlanPdf", new { id = planId })
            {
                FileName = "PlanAlimenticio_" + plan.PacienteStr + ".pdf",
                IsJavaScriptDisabled = false,
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape,
            };
        }



        public async Task<ActionResult> TraerPaciente(long? pacienteId)
        {
            if (pacienteId == null) return RedirectToAction("Error", "Home");

            var paciente = await _pacienteServicio.GetById(pacienteId.Value);

            return Json(paciente, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarPaciente(int? page, string cadenaBuscar)
        {
            ViewBag.FilterValue = cadenaBuscar;

            var establecimientoId = ObtenerEstablecimientoIdUser();

            var pageNumber = page ?? 1;
            var eliminado = false;
            var pacientes =
                await _pacienteServicio.Get(establecimientoId, eliminado, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (pacientes == null) return RedirectToAction("Error", "Home");

            return PartialView(pacientes.Select(x => new PacienteViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                Apellido = x.Apellido,
                Nombre = x.Nombre,
                Celular = x.Celular,
                Telefono = x.Telefono,
                Cuit = x.Cuit,
                Dni = x.Dni,
                FechaNac = x.FechaNac,
                FechaAlta = x.FechaAlta,
                Sexo = x.Sexo,
                Mail = x.Mail,
                Eliminado = x.Eliminado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginasModal));
        }

        public async Task<ActionResult> TraerPlan(long? planId)
        {
            if (planId == null) return RedirectToAction("Error", "Home");

            var plan = await _planAlimenticioServicio.GetById(planId.Value);

            return Json(plan, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> BuscarPlan(int? page, string cadenaBuscar)
        {
            var pageNumber = page ?? 1;
            var eliminado = false;
            var planes =
                await _planAlimenticioServicio.Get(false, !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty);

            if (planes == null) return RedirectToAction("Error", "Home");

            return PartialView(planes.Select(x => new PlanAlimenticioViewModel()
            {
                Id = x.Id,
                Codigo = x.Codigo,
                PacienteStr = x.PacienteStr,
                Motivo = x.Motivo,
                Comentarios = x.Comentarios,
                Eliminado = x.Eliminado,
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        private PlanAlimenticioDto CargarDatos(PlanAlimenticioABMViewModel vm)
        {
            return new PlanAlimenticioDto()
            {
                Id = vm.Id,
                Codigo = vm.Codigo,
                Fecha = vm.Fecha,
                Motivo = vm.Motivo,
                PacienteId = vm.PacienteId,
                PacienteStr = vm.PacienteStr,
                Comentarios = vm.Comentarios,
                Eliminado = vm.Eliminado,
                TotalCalorias = 0,
                ComentarioPacienteOP = vm.ComentarioPacienteOP,

            };
        }

        public async Task<ActionResult> ModificarRecetario(string recetario , long planId)
        {
            try
            {
                var plan = await _planAlimenticioServicio.GetById(planId);
                plan.Comentarios = recetario;
                await _planAlimenticioServicio.Update(plan);
            }
            catch (Exception ex)
            {
                return Json(new { estado = false , mensaje = ex.Message });
            }
            return Json(new {estado = true });
        }

        public async Task<ActionResult> ObtenerRecetarios(long? planAlimenticio)
        {
            var recetarios = await _planAlimenticioServicio.GetFoodsByPlanId(planAlimenticio.Value);
            var model = Mapper.Map<IEnumerable<ComidaDetalleViewModel>>(recetarios);
            return PartialView(model);
        }
    }
}
