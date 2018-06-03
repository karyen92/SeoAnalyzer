using System.Text.RegularExpressions;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using SeoAnalyzer.Core.Installer.Framework;
using SeoAnalyzer.Core.Services;

namespace SeoAnalyzer.Core.Installer
{
    public class MediatorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IMediator>()
                    .ImplementedBy<Framework.Mediator>()
                    .LifestyleTransient()
                    .DependsOn(new { container = container.Kernel }));

            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IRequestHandler<,>))
                .WithService
                .Base().LifestyleTransient());

            container.Register(Classes.FromThisAssembly()
                .BasedOn(typeof(IAsyncRequestHandler<,>))
                .WithService
                .Base().LifestyleTransient());
        }
    }
}
