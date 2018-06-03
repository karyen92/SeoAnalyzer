using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeoAnalyzer.Web.Config.Windsor;

namespace SeoAnalyzer.Web.Config.Installer
{

    public class CoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IActionInvoker>()
                    .ImplementedBy<WindsorActionInvoker>()
                    .LifestyleTransient()
                    .DependsOn(new { container = container }));
        }
    }
}