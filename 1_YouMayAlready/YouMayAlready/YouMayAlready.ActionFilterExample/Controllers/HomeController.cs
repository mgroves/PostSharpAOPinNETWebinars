using System.Web.Mvc;
using YouMayAlready.ActionFilterExample.ActionFilters;

namespace YouMayAlready.ActionFilterExample.Controllers
{
    public class HomeController : Controller
    {
        [ExampleActionFilter]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }
    }
}
