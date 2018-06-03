using System.Linq;
using System.Web;
using SeoAnalyzer.Core.Installer.Framework;
using SeoAnalyzer.Core.Mediator.Message;
using SeoAnalyzer.Core.Services;
using SeoAnalyzer.Core.Validator;

namespace SeoAnalyzer.Core.Mediator.Handler
{
    public class AnalyzeHandler : IRequestHandler<AnalyzeMessage, AnalyzeResultMessage>
    {
        private readonly IAnalyzerStrategy _analyzerStrategy;
        private readonly AnalyzeValidator _validator;
        private readonly IParseValidationResultService _parseValidationResultService;

        public AnalyzeHandler(IAnalyzerStrategy analyzerStrategy, AnalyzeValidator validator, IParseValidationResultService parseValidationResultService)
        {
            _analyzerStrategy = analyzerStrategy;
            _validator = validator;
            _parseValidationResultService = parseValidationResultService;
        }

        public AnalyzeResultMessage Handle(AnalyzeMessage message)
        {
            var result = _validator.Validate(message);
            var output = new AnalyzeResultMessage
            {
                ValidationResult = _parseValidationResultService.Parse(result),
                Success = result.IsValid,
                Category = message.Category.ToString()
            };

            if (result.IsValid)
            {
                var path = System.Configuration.ConfigurationManager.AppSettings["StopWordListPath"];
                var absPath = HttpContext.Current.Server.MapPath(path);
                var service = _analyzerStrategy.GetService(message.Category);
                var analyzeResult = service.Analyze(message.Input, absPath);

                output.Links = analyzeResult.Links.Select(x => new AnalysisDto
                {
                    Word = x.Key,
                    Count = x.Value
                });
                output.Meta = analyzeResult.Meta.Select(x => new AnalysisDto
                {
                    Word = x.Key,
                    Count = x.Value
                });
                output.Words = analyzeResult.Words.Select(x => new AnalysisDto
                {
                    Word = x.Key,
                    Count = x.Value
                });
            }

            return output;
        }
    }
}
