using System.Collections.Generic;

namespace SeoAnalyzer.Core.Domain
{
    public class AnalysisResult
    {
        public Dictionary<string, int> Words = new Dictionary<string, int>();
        public Dictionary<string, int> Links = new Dictionary<string, int>();
        public Dictionary<string, int> Meta = new Dictionary<string, int>();
    }
}
