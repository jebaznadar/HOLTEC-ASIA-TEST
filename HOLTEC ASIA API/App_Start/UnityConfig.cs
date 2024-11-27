using HOLTEC_ASIA_API.Models;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace HOLTEC_ASIA_API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<iemployeerepository, employeerepository>();
            container.RegisterType<iemployeebl, employeebl>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}