using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NutricionWeb.Models;
using Servicio.Interface.Rol;
using Servicio.Interface.Usuario;
using Servicio.Rol;
using Servicio.Usuario;

namespace NutricionWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IRolServicio _rolServicio;
        private readonly IUsuarioServicio _usuarioServicio;

        public MvcApplication(IRolServicio rolServicio, IUsuarioServicio usuarioServicio)
        {
            _rolServicio = rolServicio;
            _usuarioServicio = usuarioServicio;
        }

        public MvcApplication()
            : this(new RolServicio(), new UsuarioServicio())
        {

        }

        protected void Application_Start()
        {
            var db = new ApplicationDbContext();
            CrearRoles(db);
            CrearSuperUsusario(db);
            AsignarSuperUsuarioRoles(db);
            db.Dispose();

            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AsignarSuperUsuarioRoles(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var superUsuario = userManager.FindByName("powernutri@asp.com.ar");

            if (!userManager.IsInRole(superUsuario.Id, "Administrador"))
            {
                userManager.AddToRole(superUsuario.Id, "Administrador");
            }

            if (!userManager.IsInRole(superUsuario.Id, "Paciente"))
            {
                userManager.AddToRole(superUsuario.Id, "Paciente");
            }

            if (!userManager.IsInRole(superUsuario.Id, "Empleado"))
            {
                userManager.AddToRole(superUsuario.Id, "Empleado");
            }

            //if (!userManager.IsInRole(superUsuario.Id, "Insert"))
            //{
            //    userManager.AddToRole(superUsuario.Id, "Insert");
            //}

            //if (!userManager.IsInRole(superUsuario.Id, "Update"))
            //{
            //    userManager.AddToRole(superUsuario.Id, "Update");
            //}

            //if (!userManager.IsInRole(superUsuario.Id, "Delete"))
            //{
            //    userManager.AddToRole(superUsuario.Id, "Delete");
            //}

            //if (!userManager.IsInRole(superUsuario.Id, "View"))
            //{
            //    userManager.AddToRole(superUsuario.Id, "View");
            //}
        }

        private void CrearSuperUsusario(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var user = userManager.FindByName("powernutri@asp.com.ar");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "powernutri@asp.com.ar",
                    Email = "powernutri@asp.com.ar"
                };
                var result = userManager.Create(user, "50Cent");
            }
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Administrador"))
            {
                var result = roleManager.Create(new IdentityRole("Administrador"));
            }

            if (!roleManager.RoleExists("Paciente"))
            {
                var result = roleManager.Create(new IdentityRole("Paciente"));
            }

            if (!roleManager.RoleExists("Empleado"))
            {
                var result = roleManager.Create(new IdentityRole("Empleado"));
            }

            //if (!roleManager.RoleExists("Insert"))
            //{
            //    var result = roleManager.Create(new IdentityRole("Insert"));
            //}

            //if (!roleManager.RoleExists("Update"))
            //{
            //    var result = roleManager.Create(new IdentityRole("Update"));
            //}

            //if (!roleManager.RoleExists("Delete"))
            //{
            //    var result = roleManager.Create(new IdentityRole("Delete"));
            //}

            //if (!roleManager.RoleExists("View"))
            //{
            //    var result = roleManager.Create(new IdentityRole("View"));
            //}


        }
    }
}
