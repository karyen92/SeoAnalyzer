using System;
using System.Collections.Generic;
using FluentValidation.Results;
using SeoAnalyzer.Core.Domain;
using SeoAnalyzer.Core.Installer.Framework;

namespace SeoAnalyzer.Core.Mediator.Message
{
    public class AnalyzeMessage : IRequest<AnalyzeResultMessage>
    {
        public AnalysisCategory Category { get; set; }
        public string Input { get; set; }
    }

    public class AnalysisDto
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }

    public class AnalyzeResultMessage
    {
        public bool Success { get; set; }
        public Dictionary<string, string> ValidationResult { get; set; }
        public IEnumerable<AnalysisDto> Words { get; set; }
        public IEnumerable<AnalysisDto> Links { get; set; }
        public IEnumerable<AnalysisDto> Meta { get; set; }

        public string Category { get; set; }
    }
}
