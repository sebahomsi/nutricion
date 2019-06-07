using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using NutricionWeb.Controllers;
using NutricionWeb.Helpers.Establecimiento;
using NutricionWeb.Helpers.Persona;
using NutricionWeb.Models;
using System.Web;
using System.Web.Mvc;
using NutricionWeb.Helpers.SubGrupo;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;


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

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new InjectionConstructor(new ApplicationDbContext()));

            container.RegisterType<AccountController>(
                new InjectionConstructor(typeof(ApplicationUserManager), typeof(ApplicationSignInManager)));

            container.RegisterType<AccountController>(new InjectionConstructor());

            container.RegisterType<UserManager<ApplicationUser>>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new HierarchicalLifetimeManager()); //

            container.RegisterType<IComboBoxSexo, ComboBoxSexo>();
            container.RegisterType<IComboBoxEstablecimiento, ComboBoxEstablecimiento>();
            container.RegisterType<IComboBoxSubGrupo, ComboBoxSubGrupo>();

            Aplicacion.IoC.UnityConfig.RegisterComponents(container);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}