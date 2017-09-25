using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using System.Configuration;
using System.Security.Principal;
using System.Threading;
using System.Web.Mvc;
using TestingList.Interfaces;
using TestingList.Services;
using TestingList.Providers;

namespace TestingList
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IListService, BaseService>();

            container.RegisterType<IDataProvider, SqlDataProvider>(
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString));


            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

        }
    }
}