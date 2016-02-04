using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http.Results;
using JustEat.Code.Api.Controllers;
using JustEat.Code.Api.DTOs;
using JustEat.Code.Api.Services;

namespace JustEat.Code.Api.Tests.Controllers
{
    [TestFixture]
    public class RestaurantControllerIntegration
    {
        private RestaurantController _controller;
        private const string Category = "Integration";

        [SetUp]
        public void TestInit()
        {
            var apiUrl = ConfigurationManager.AppSettings["justeat-api-endpoint"];
            var restClient = new RestClient(apiUrl);
            var service = new RestaurantService(restClient);
            _controller = new RestaurantController(service);
        }
     
        [TestCase("SE16")]
        [TestCase("SE19")]
        [Category(Category)]
        public void Get_Should_Return_A_RestaurantDTO_List(string outcode)
        {
            var actual = _controller.Get("SE16") as OkNegotiatedContentResult<IEnumerable<RestaurantDTO>>;

            if (!actual.Content.Any())
            {
                Assert.Fail();
            }
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_A_RestaurantDTO_List_With_No_NullOrEmpty_Names()
        {
            var actual = _controller.Get("SE16") as OkNegotiatedContentResult<IEnumerable<RestaurantDTO>>;

            var nameCollection = actual.Content.Select(x => x.Name);

            foreach (string name in nameCollection)
            {
                if (string.IsNullOrEmpty(name))
                {
                    Assert.Fail();
                }
            }

        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_A_RestaurantDTO_List_With_One_Or_More_CusineType_List_Items()
        {
            var actual = _controller.Get("SE16") as OkNegotiatedContentResult<IEnumerable<RestaurantDTO>>;

            var cusineTypeCollection = actual.Content.Select(x => x.CusineTypes);

            foreach (IEnumerable<string> cusineType in cusineTypeCollection)
            {
                if (string.IsNullOrEmpty(cusineType.First()))
                {
                    Assert.Fail();
                }
            }

        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_A_RestaurantDTO_List_With_One_Or_More_RestaurantDTO_Object_With_Rating_Distinc_Of_Default_Value()
        {
            var actual = _controller.Get("SE16") as OkNegotiatedContentResult<IEnumerable<RestaurantDTO>>;

            var ratingCollection = actual.Content.Select(x => x.Rating);

            int defaultValuesCounter = 0;
            foreach (double rating in ratingCollection)
            {
                if (rating == 0.0)
                {
                    defaultValuesCounter++;
                }
            }

            if (ratingCollection.Count() == defaultValuesCounter)
            {
                Assert.Fail();
            }

        }
    }
}
