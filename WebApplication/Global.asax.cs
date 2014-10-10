using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System.Diagnostics.Tracing;
using WebApplication.Sinks;
using WebApplication.Logging;

namespace WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //HostingEnvironment.QueueBackgroundWorkItem(token => SemanticLogger(token));
            var listener = new ObservableEventListener();
            listener.EnableEvents(SemanticLogging.EventSource, EventLevel.LogAlways, Keywords.All);
            listener.LogToDatabase();
        }

        #region Out-Of-Process
        //void SemanticLogger(CancellationToken token)
        //{
        //    var source = new TraceSource("WebApplication");
        //    var timer = new System.Timers.Timer(TimeSpan.FromSeconds(1).TotalMilliseconds) { AutoReset = true };
        //    try
        //    {
        //        var configuration = TraceEventServiceConfiguration.Load(ConfigurationManager.AppSettings["SemanticLoggingConfigurationFile"]);
        //        using (var service = new TraceEventService(configuration))
        //        {
        //            timer.Elapsed += (sender, args) =>
        //            {
        //                if (token.IsCancellationRequested)
        //                    service.Stop();
        //            };
        //            timer.Start();
        //            service.Start();
        //        }
        //    }
        //    finally
        //    {
        //        timer.Stop();
        //        source.Close();
        //    }
        //} 
        #endregion
    }
}
