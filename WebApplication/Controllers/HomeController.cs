using System.Web.Mvc;
using WebApplication.Logging;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SemanticLogging.EventSource.Index();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            SemanticLogging.EventSource.About();
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            SemanticLogging.EventSource.Contact();
            return View();
        }
    }
}