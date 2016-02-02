using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace JustEat.Code.Api.ExceptionFilterAttributes
{

    public class UnexpectedExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
