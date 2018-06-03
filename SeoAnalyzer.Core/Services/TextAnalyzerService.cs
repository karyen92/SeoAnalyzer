using System;
using System.IO;
using System.Text.RegularExpressions;
using SeoAnalyzer.Core.Domain;

namespace SeoAnalyzer.Core.Services
{
    public class TextAnalyzerService : IAnalyzerService
    {
        public AnalysisResult Analyze(string text, string path)
        {
            var lines = File.ReadAllText(path);
            var pattern = @"(?<=(\A|\s|\.|,|!|\?))(" +
                          lines +
                          @")(?=(\s|\z|\.|,|!|\?))";
            var clean  = Regex.Replace(text, pattern, "");

            var result = new AnalysisResult();
            char[] delimiters = { ' ', '\r', '\n' };
            var splits = clean.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            foreach (var split in splits)
            {
                if (result.Words.ContainsKey(split))
                {
                    result.Words[split] += 1;
                }
                else
                {
                    result.Words.Add(split, 1);
                }
               
            }
            return result;
        }

        public AnalysisCategory GetCategory()
        {
            return AnalysisCategory.Text;
        }
    }
}
