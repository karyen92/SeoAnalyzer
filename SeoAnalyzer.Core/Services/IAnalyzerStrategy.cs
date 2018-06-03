using SeoAnalyzer.Core.Domain;

namespace SeoAnalyzer.Core.Services
{
    public interface IAnalyzerStrategy
    {
        IAnalyzerService GetService(AnalysisCategory category);
    }
}
