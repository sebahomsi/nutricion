using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NutricionWeb.Helpers.AutoMapper;
using NutricionWeb.Models;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NutricionWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public MvcApplication()
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

            if (!userManager.IsInRole(superUsuario.Id, "SuperAdmin"))
            {
                userManager.AddToRole(superUsuario.Id, "SuperAdmin");
            }
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
                _ = userManager.Create(user, "123456");
            }
        }

        private void CrearRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("SuperAdmin"))
            {
                _ = roleManager.Create(new IdentityRole("SuperAdmin"));
            }

            if (!roleManager.RoleExists("Administrador"))
            {
                _ = roleManager.Create(new IdentityRole("Administrador"));
            }

            if (!roleManager.RoleExists("Paciente"))
            {
                _ = roleManager.Create(new IdentityRole("Paciente"));
            }

            if (!roleManager.RoleExists("Empleado"))
            {
                _ = roleManager.Create(new IdentityRole("Empleado"));
            }

            if (!roleManager.RoleExists("GestionPacientes"))
            {
                _ = roleManager.Create(new IdentityRole("GestionPacientes"));
            }
            if (!roleManager.RoleExists("GestionTurnos"))
            {
                _ = roleManager.Create(new IdentityRole("GestionTurnos"));
            }
        }
    }
}
