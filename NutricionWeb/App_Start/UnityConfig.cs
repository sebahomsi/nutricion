using System.Web.Mvc;
using Unity;
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
                new Microsoft.Practices.Unity.InjectionFactory(c => HttpContext.Current.GetOwinContext().Authentication));

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new Microsoft.Practices.Unity.InjectionConstructor(typeof(ApplicationDbContext)));

            container.RegisterType<AccountController>(
                new Microsoft.Practices.Unity.InjectionConstructor(typeof(ApplicationUserManager), typeof(ApplicationSignInManager)));

            container.RegisterType<AccountController>(new Microsoft.Practices.Unity.InjectionConstructor());

            container.RegisterType<UserManager<ApplicationUser>>(new Microsoft.Practices.Unity.HierarchicalLifetimeManager());

            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>(
                new Microsoft.Practices.Unity.HierarchicalLifetimeManager()); //

            Aplicacion.IoC.UnityConfig.RegisterComponents();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}