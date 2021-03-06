﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;

namespace SeoAnalyzer.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ContainerConfig.Register(GlobalConfiguration.Configuration, out _container);
            
        }
    }
}
