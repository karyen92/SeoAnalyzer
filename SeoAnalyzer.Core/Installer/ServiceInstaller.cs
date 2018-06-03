using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeoAnalyzer.Core.Services;

namespace SeoAnalyzer.Core.Installer
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn(typeof(IAnalyzerService)).WithServiceAllInterfaces().LifestyleTransient());
            container.Register(Component.For<IAnalyzerStrategy>().ImplementedBy<AnalyzerStrategy>().LifestyleTransient());
            container.Register(Component.For<IParseValidationResultService>().ImplementedBy<ParseValidationResultService>().LifestyleTransient());
        }
    }
}
