using JustEat.Code.Api.Services;
using RestSharp;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using System.Configuration;
using System.Web.Http;

namespace JustEat.Code.Api.App_Start
{
    public class Ioc
    {
        private static readonly string _apiUrl = ConfigurationManager.AppSettings["justeat-api-endpoint"];

        public static void Init()
        {
            var container = new Container();

            container.Register<IRestaurantService, RestaurantService>(Lifestyle.Transient);
            container.Register<IRestClient>(() => new RestClient(_apiUrl), Lifestyle.Transient);
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver =
                new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}