using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using SeoAnalyzer.Core.Domain;

namespace SeoAnalyzer.Core.Services
{
    public class WebPageAnalyzerService : IAnalyzerService
    {
        public AnalysisResult Analyze(string url, string path)
        {
            
            var web = new HtmlWeb();
            var result = new AnalysisResult();

            var htmlDoc = web.Load(url);

            var text = "";
            var nodes = htmlDoc.DocumentNode.Descendants().Where(n =>
                n.NodeType == HtmlNodeType.Text &&
                n.ParentNode.Name != "script" &&
                n.ParentNode.Name != "style");

            foreach (var node in nodes)
            {
                text += node.InnerText;
            }

            var metaNodes = htmlDoc.DocumentNode.SelectNodes("//meta");
            foreach (var node in metaNodes)
            {
                var content = node.GetAttributeValue("content", "");

                if (!string.IsNullOrWhiteSpace(content) && !result.Meta.ContainsKey(content.ToLowerInvariant()))
                {
                    result.Meta.Add(content.ToLowerInvariant(), 0);
                }
            }

            var linkNodes = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
            foreach (var node in linkNodes)
            {
                var attribute = node.Attributes["href"];
                if (!string.IsNullOrWhiteSpace(attribute.Value) && !result.Links.ContainsKey(attribute.Value) && (attribute.Value.IndexOf("http://") == 0 || attribute.Value.IndexOf("https://") == 0))
                {
                    result.Links.Add(attribute.Value, 1);
                }
            }


            var lines = File.ReadAllText(path);
            var pattern = @"(?<=(\A|\s|\.|,|!|\?))(" +
                          lines +
                          @")(?=(\s|\z|\.|,|!|\?))";
            var cleanFromStopWords  = Regex.Replace(text, pattern, "");

            var clean = Regex.Replace(cleanFromStopWords, @"\s\s+", "");

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

                if (result.Meta.ContainsKey(split.ToLowerInvariant()))
                {
                    result.Meta[split] += 1;
                }
            }

           
            return result;
        }

        public AnalysisCategory GetCategory()
        {
            return AnalysisCategory.Webpage;
        }
    }
}
