using System.Net;
using System.Net.Http;
using System.Web.Http;
using SeoAnalyzer.Core.Installer.Framework;
using SeoAnalyzer.Core.Mediator.Message;

namespace SeoAnalyzer.Web.Api
{
    [RoutePrefix("api/analyzer")]
    public class AnalyzerController : ApiController
    {
        private readonly IMediator _mediator;

        public AnalyzerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage Post(AnalyzeMessage message)
        {
            var output = _mediator.Send(message);
            return Request.CreateResponse(output);
        }
    }
}
