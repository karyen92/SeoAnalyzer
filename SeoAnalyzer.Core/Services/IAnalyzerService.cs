using SeoAnalyzer.Core.Domain;

namespace SeoAnalyzer.Core.Services
{
    public interface IAnalyzerService
    {
        AnalysisResult Analyze(string text, string path);
        AnalysisCategory GetCategory();
    }
}
