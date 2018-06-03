using System.Collections.Generic;
using System.Linq;
using SeoAnalyzer.Core.Domain;

namespace SeoAnalyzer.Core.Services
{
    public class AnalyzerStrategy : IAnalyzerStrategy
    {
        private readonly IEnumerable<IAnalyzerService> _services;

        public AnalyzerStrategy(IEnumerable<IAnalyzerService> services)
        {
            _services = services;
        }

        public IAnalyzerService GetService(AnalysisCategory category)
        {
            return _services.First(x => x.GetCategory() == category);
        }
    }
}
