using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SeoAnalyzer.Web
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApiGet",
                "api/{controller}/{id}",
                new { action = "Get" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            config.Routes.MapHttpRoute("DefaultApiGetAll",
                "api/{controller}",
                new { action = "Get" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });


            config.Routes.MapHttpRoute("DefaultApiPost",
                "api/{controller}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });

            config.Routes.MapHttpRoute("CustomApiPost",
                "api/{controller}/{action}",
                new { action = "Post" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Post) });


            config.Routes.MapHttpRoute("DefaultApiPut",
                "api/{controller}/{id}",
                new { action = "Put", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });

            config.Routes.MapHttpRoute("CustomApiPut",
                "api/{controller}/{action}/{id}",
                new { action = "Put", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Put) });


            config.Routes.MapHttpRoute("DefaultApiDelete",
                "api/{controller}/{id}",
                new { action = "Delete" },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Delete) });


            config.Routes.MapHttpRoute("CustomApi",
                "api/{controller}/{action}/{id}",
                new { action = "Get", id = RouteParameter.Optional },
                new { httpMethod = new HttpMethodConstraint(HttpMethod.Get) });

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        }
    }
}
