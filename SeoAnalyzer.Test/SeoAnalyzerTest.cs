using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Core;
using Castle.Windsor;
using Castle.Windsor.Installer;
using SeoAnalyzer.Core.Domain;
using SeoAnalyzer.Core.Services;

namespace SeoAnalyzer.Test
{
    [TestFixture]
    public class SeoAnalyzerTest
    {
        private IWindsorContainer Container { get; set; }

        [SetUp]
        public void Setup()
        {
            Container = new WindsorContainer()
                .Install(FromAssembly.This(),
                    FromAssembly.Named("SeoAnalyzer.Core")
                );

            var assemblies = new List<Assembly>
            {
                Assembly.GetExecutingAssembly()
            };

            var relatedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Where(x => x.Name.StartsWith("SeoAnalyzer."));
            assemblies.AddRange(relatedAssemblies.Select(Assembly.Load));

        }

        [Test]
        public void TextAnalyzerTest1()
        {
            var services = Container.ResolveAll<IAnalyzerService>();
            var path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            var textAnalyzerService = services.First(x => x.GetCategory() == AnalysisCategory.Text);
            var result = textAnalyzerService.Analyze("Hello to the Alice", path);
            
            Assert.AreEqual(result.Words.Count, 2);
          
        }

        [Test]
        public void TextAnalyzerTest2()
        {
            var services = Container.ResolveAll<IAnalyzerService>();
            var path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            var textAnalyzerService = services.First(x => x.GetCategory() == AnalysisCategory.Text);
            var result = textAnalyzerService.Analyze("   & abyss", path);


            Assert.AreEqual(result.Words.Count, 1);

        }

        [Test]
        public void WebPageAnalyzerTest1()
        {
            var services = Container.ResolveAll<IAnalyzerService>();
            var path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            var textAnalyzerService = services.First(x => x.GetCategory() == AnalysisCategory.Webpage);
            var result = textAnalyzerService.Analyze("http://example.webscraping.com/", path);
            
            Assert.AreEqual(result.Words.Count, 21);
        }

        [Test]
        public void WebPageAnalyzerTest2()
        {
            var services = Container.ResolveAll<IAnalyzerService>();
            var path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            var textAnalyzerService = services.First(x => x.GetCategory() == AnalysisCategory.Webpage);
            var result = textAnalyzerService.Analyze("http://example.webscraping.com/", path);

            Assert.AreEqual(result.Meta.Count, 5);
        }

        [Test]
        public void WebPageAnalyzerTest3()
        {
            var services = Container.ResolveAll<IAnalyzerService>();
            var path = System.Configuration.ConfigurationManager.AppSettings["Path"];

            var textAnalyzerService = services.First(x => x.GetCategory() == AnalysisCategory.Webpage);
            var result = textAnalyzerService.Analyze("http://example.webscraping.com/", path);
            
            Assert.AreEqual(result.Links.Count, 0);
        }
    }
}
