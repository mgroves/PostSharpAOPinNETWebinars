using System.Web;

namespace YouMayAlready.HttpModuleExample.Modules
{
    public class ExampleHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new System.EventHandler(context_BeginRequest);
            context.EndRequest += new System.EventHandler(context_EndRequest);
        }

        public void Dispose()
        {
        }

        void context_BeginRequest(object sender, System.EventArgs e)
        {
            HttpContext.Current.Response.Write("hello");
            HttpContext.Current.Response.AddHeader("MyCustomHeader", "MyCustomHeaderValue");
        }

        void context_EndRequest(object sender, System.EventArgs e)
        {
            HttpContext.Current.Response.Write("goodbye");
        }
    }
}