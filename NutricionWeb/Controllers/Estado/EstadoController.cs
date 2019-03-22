using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using NutricionWeb.Models.Estado;
using PagedList;
using Servicio.Interface.Estado;
using static NutricionWeb.Helpers.PagedList;

namespace NutricionWeb.Controllers.Estado
{
    public class EstadoController : Controller
    {
        private readonly IEstadoServicio _estadoServicio;

        public EstadoController(IEstadoServicio estadoServicio)
        {
            _estadoServicio = estadoServicio;
        }

        // GET: Estado
        public async Task<ActionResult> Index(int? page,string cadenaBuscar)
        {
            var pageNumber = page ?? 1;

            var datos = await _estadoServicio.Get(!string.IsNullOrEmpty(cadenaBuscar)? cadenaBuscar:string.Empty);

            var estados = Mapper.Map<IEnumerable<EstadoViewModel>>(datos);

            return View(estados.ToPagedList(pageNumber,CantidadFilasPorPaginas));
        }
    }
}