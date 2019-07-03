using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NutricionWeb.Models;
using NutricionWeb.Models.Seguridad;
using PagedList;
using static NutricionWeb.Helpers.PagedList;


namespace NutricionWeb.Controllers.Usuario
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index(string cadenaBuscar, int? page)
        {
            var pageNumber = (page ?? 1);

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            var cadena = !string.IsNullOrEmpty(cadenaBuscar) ? cadenaBuscar : string.Empty;
            ViewBag.FilterValue = cadenaBuscar;

            var usuarios = userManager.Users.Where(x => x.Email.Contains(cadena)
                                                        || x.UserName.Contains(cadena))
                .Select(x => new UsuarioViewModel()
                {
                    UsuarioId = x.Id,
                    Nombre = x.UserName,
                    EMail = x.Email
                }).ToList();

            return View(usuarios.ToPagedList(pageNumber, CantidadFilasPorPaginas));
            
        }


        // ============================================================================================= //
        // ===========================     Usuario - Roles    ========================================== //
        // ============================================================================================= //

        public async Task<ActionResult> ObtenerRolesPorUsuario(string usuarioId)
        {
            if (string.IsNullOrEmpty(usuarioId)) return RedirectToAction("Error", "Home");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

            var usuarioSeleccionado = await userManager.FindByIdAsync(usuarioId);

            if (usuarioSeleccionado == null) return RedirectToAction("Error", "Home");

            var roles = roleManager.Roles.ToList();

            var usuario = new UsuarioViewModel
            {
                UsuarioId = usuarioSeleccionado.Id,
                Nombre = usuarioSeleccionado.UserName,
                EMail = usuarioSeleccionado.Email
            };

            foreach (var rol in usuarioSeleccionado.Roles)
            {
                usuario.Roles.Add(new RolViewModel()
                {
                    RolId = rol.RoleId,
                    Nombre = roles.First(x => x.Id == rol.RoleId).Name
                });
            }

            return View(usuario);
        }

        public async Task<ActionResult> EliminarRolDelUsuario(string usuarioId, string rolId)
        {
            if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(rolId)) return RedirectToAction("Error", "Home");

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

            var usuario = await userManager.FindByIdAsync(usuarioId);

            if (usuario == null) return RedirectToAction("Error", "Home");

            var rol = await roleManager.FindByIdAsync(rolId);

            if (rol == null) return RedirectToAction("Error", "Home");

            if (userManager.IsInRole(usuario.Id, rol.Name))
            {
                await userManager.RemoveFromRoleAsync(usuario.Id, rol.Name);
            }

            return RedirectToAction("ObtenerRolesPorUsuario", "Usuarios", new { usuarioId = usuarioId });
        }

        public ActionResult AgregarRol(string usuarioId)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));

            var usuarioSeleccionado = userManager.FindById(usuarioId);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

            var usuarioRol = new UsuarioRolViewModel();
            usuarioRol.UsuarioId = usuarioId;
            usuarioRol.Usuario = usuarioSeleccionado.UserName;
            usuarioRol.Roles = new SelectList(roleManager.Roles, "Id", "Name");

            return View(usuarioRol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgregarRol(UsuarioRolViewModel vm)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));

            var rol = roleManager.FindById(vm.RolId);
            var usuario = userManager.FindById(vm.UsuarioId);

            if (!userManager.IsInRole(vm.UsuarioId, rol.Name))
            {
                await userManager.AddToRoleAsync(vm.UsuarioId, rol.Name);
            }

            return RedirectToAction("ObtenerRolesPorUsuario", "Usuarios", new { usuarioId = vm.UsuarioId });
        }

        // ============================================================================================= //

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
