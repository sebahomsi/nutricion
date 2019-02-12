using NutricionWeb.Helpers.Persona;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Web;
using Unity.Injection;
using Unity.Lifetime;
using NutricionWeb.Controllers;
using NutricionWeb.Models;
using static Aplicacion.Conexion.ConexionDb;


namespace NutricionWeb
{
    public static class UnityConfig
    {

        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ApplicationDbContext>();
            container.RegisterType<ApplicationSignInManager>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<EmailService>();

            container.RegisterType<IAuthenticationManager>(
                new InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            //container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
            //    new InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<AccountController>(
                new InjectionConstructor(typeof(ApplicationUserManager), typeof(ApplicationSignInManager)));

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager()); //

            container.RegisterType<IComboBoxSexo, ComboBoxSexo>();

            Aplicacion.IoC.UnityConfig.RegisterComponents(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}