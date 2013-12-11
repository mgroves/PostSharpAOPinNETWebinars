using System.Web.Mvc;

namespace YouMayAlready.ActionFilterExample.ActionFilters
{
    public class ExampleActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<h2>hello</h2>");
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.Message += " I'm glad you're here!";
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write("<h2>goodbye</h2>");
        }
    }
}