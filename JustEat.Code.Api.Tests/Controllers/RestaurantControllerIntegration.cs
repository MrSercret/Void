using NUnit.Framework;
using RestSharp;
using RestSharp.Deserializers;
using System.Collections.Generic;
using System.Configuration;

namespace JustEat.Code.Api.Tests.Controllers
{
    [TestFixture]
    public class RestaurantControllerIntegration
    {
        private readonly string _appliationUrl;
        private const string Category = "Integration";

        public RestaurantControllerIntegration()
        {
            _appliationUrl = ConfigurationManager.AppSettings["application_url"];
        }

        [TestCase("SE16")]
        [TestCase("SE19")]
        [Category(Category)]
        public void Get_Should_Return_Valid_Json_With_Real_Outcode(string outcode)
        {
            var actual = GetIRestResponse("SE16");

            var dsr = new JsonDeserializer();

            var deserialized = dsr.Deserialize<object>(actual);

            Assert.IsNotNull(deserialized);
        }

        [TestCase("Name")]
        [TestCase("Rating")]
        [TestCase("CusineTypes")]
        [Category(Category)]
        public void Get_Should_Return_Valid_Json_Containing_Property(string value)
        {
            var actual = GetIRestResponse("SE16");

            var dsr = new JsonDeserializer();

            var deserialized = dsr.Deserialize<JsonArray>(actual)[0];

            if (!((IDictionary<string, object>)deserialized).ContainsKey(value))
            {
                Assert.Fail();
            }

        }

        private IRestResponse GetIRestResponse(string outcode)
        {
            IRestClient client = new RestClient(_appliationUrl);

            var request = new RestRequest("api/restaurant/{outcode}", Method.GET);
            request.AddParameter("outcode", outcode);
            request.JsonSerializer.ContentType = "application/json; charset=utf-8";

            return client.Execute(request);
        }
    }
}
