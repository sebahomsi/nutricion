﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NutricionWeb.Models;
using Servicio.Interface.Rol;
using Servicio.Interface.Usuario;
using Servicio.Rol;
using Servicio.Usuario;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NutricionWeb.Helpers.AutoMapper;

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
            IniciarAutoMapper.Iniciar();
        }

        private void AsignarSuperUsuarioRoles(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            var superUsuario = userManager.FindByName("powernutri@asp.com.ar");

            if (!userManager.IsInRole(superUsuario.Id, "Administrador"))
            {
                userManager.AddToRole(superUsuario.Id, "Administrador");
            }
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
                var result = userManager.Create(user, "123456");
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

            if (!roleManager.RoleExists("GestionPacientes"))
            {
                var result = roleManager.Create(new IdentityRole("GestionPacientes"));
            }
            if (!roleManager.RoleExists("GestionTurnos"))
            {
                var result = roleManager.Create(new IdentityRole("GestionTurnos"));
            }
        }
    }
}
