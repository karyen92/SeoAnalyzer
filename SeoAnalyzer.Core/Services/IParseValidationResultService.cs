using System.Collections.Generic;
using FluentValidation.Results;

namespace SeoAnalyzer.Core.Services
{
    public interface IParseValidationResultService
    {
        Dictionary<string, string> Parse(ValidationResult result);
    }
}
