using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System;
using System.Configuration;
using System.Diagnostics.Tracing;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApplication.Logging;
using WebApplication.Sinks;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly ObservableEventListener eventListener = new ObservableEventListener();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RegisterEvents();
        }

        protected void Application_End()
        {
            UnregisterEvents();
        }

        private void UnregisterEvents()
        {
            eventListener.DisableEvents(SemanticLogging.EventSource);
        }

        private void RegisterEvents()
        {
            TimeSpan flushInterval = TimeSpan.FromSeconds(10);
            var flushIntervalSetting = ConfigurationManager.AppSettings["SemanticLogging:Database:FlushInterval"];
            if (!string.IsNullOrEmpty(flushIntervalSetting))
                TimeSpan.TryParse(flushIntervalSetting, out flushInterval);
            eventListener.EnableEvents(SemanticLogging.EventSource, EventLevel.LogAlways, Keywords.All);
            eventListener.LogToDatabase(flushInterval);
        }
    }
}
