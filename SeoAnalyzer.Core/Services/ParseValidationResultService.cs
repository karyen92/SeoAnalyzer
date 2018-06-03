using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace SeoAnalyzer.Core.Services
{
    public class ParseValidationResultService: IParseValidationResultService
    {
        public Dictionary<string, string> Parse(ValidationResult result)
        {
            var errors = new Dictionary<string, string>();
          
            if (result.Errors != null)
            {
                foreach (var error in result.Errors)
                {
                    if (errors.Any(x => x.Key == error.PropertyName))
                    {
                        errors[error.PropertyName] += " | " +  error.ErrorMessage;
                    }
                    else
                    {
                        errors.Add(error.PropertyName, error.ErrorMessage);
                    }
                }
            }

            return errors;
        }
    }
}
