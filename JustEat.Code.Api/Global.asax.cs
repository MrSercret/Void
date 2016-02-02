using System.Web;
using System.Web.Http;
using JustEat.Code.Api.App_Start;

namespace JustEat.Code.Api
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            Ioc.Init();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }
    }
}