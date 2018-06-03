using FluentValidation;
using SeoAnalyzer.Core.Domain;
using SeoAnalyzer.Core.Mediator.Message;
using SeoAnalyzer.Core.Validator.Validation;

namespace SeoAnalyzer.Core.Validator
{
    public class AnalyzeValidator : AbstractValidator<AnalyzeMessage>
    {
        public AnalyzeValidator()
        {
            RuleFor(x => x.Input).NotEmpty().WithMessage("Please fill in text or url");
            RuleFor(x => x.Input).SetValidator(new ValidUrlValidation()).When(x => x.Category == AnalysisCategory.Webpage);
            RuleFor(x => x.Category).Must(ValidCategory).WithMessage("Please select a valid option");
        }

        private bool ValidCategory(AnalysisCategory category)
        {
            return category == AnalysisCategory.Webpage || category == AnalysisCategory.Text;
        }
    }
}
