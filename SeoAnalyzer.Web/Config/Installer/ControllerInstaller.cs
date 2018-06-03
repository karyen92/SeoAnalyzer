using System.Web.Http;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeoAnalyzer.Web.Config.Windsor;

namespace SeoAnalyzer.Web.Config.Installer
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                .BasedOn<IController>()
                .LifestyleTransient());

            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient());

            container.Register(Component.For<System.Web.Http.Filters.IFilterProvider>()
                .ImplementedBy<WindsorFilterProvider>());
        }
    }
}