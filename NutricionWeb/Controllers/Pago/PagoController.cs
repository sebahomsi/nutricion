using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using NutricionWeb.Models.Paciente;
using NutricionWeb.Models.Pago;
using PagedList;
using Servicio.Interface.Paciente;
using Servicio.Interface.Pago;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Pago
{
    public class PagoController : Controller
    {
        private readonly IPagoServicio _pagoServicio;
        private readonly IPacienteServicio _pacienteServicio;

        public PagoController(IPagoServicio pagoServicio, IPacienteServicio pacienteServicio)
        {
            _pagoServicio = pagoServicio;
            _pacienteServicio = pacienteServicio;
        }

        
        // GET: Pago
        public async Task<ActionResult> Index(int? page)
        {
            var pageNumber = page ?? 1;
            var pagos =await _pagoServicio.GetByDate(DateTime.Today);
            if (pagos == null) return HttpNotFound();

            return View(pagos.Select(x=>new PagoViewModel()
            {
                Id = x.Id,
                EstaEliminado = x.EstaEliminado,
                Concepto = x.Concepto,
                Monto = x.Monto,
                Fecha = x.Fecha,
                PacienteId = x.PacienteId,
                PacienteStr = x.PacienteStr,
                Codigo = x.Codigo,
                
            }).ToPagedList(pageNumber, CantidadFilasPorPaginas));
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            return View(new PagoViewModel());
        }

        // POST: Pago/Create
        [HttpPost]
        public async Task<ActionResult> Create(PagoViewModel vm)
        {
            try
            {
                var dto = CargarDatos(vm);
                await _pagoServicio.Add(dto);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(vm);
            }
            return RedirectToAction("Index");
        }

        private PagoDto CargarDatos(PagoViewModel vm)
        {
           return new PagoDto()
           {
               Id = vm.Id,
               EstaEliminado = vm.EstaEliminado,
               Concepto = vm.Concepto,
               Codigo = vm.Codigo,
               PacienteId = vm.PacienteId,
               PacienteStr = vm.PacienteStr,
               Fecha = vm.Fecha,
               Monto = vm.Monto,
           };
        }

        // GET: Pago/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pago/Edit/5
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

        // GET: Pago/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pago/Delete/5
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
