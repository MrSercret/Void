using JustEat.Code.Api.Controllers;
using JustEat.Code.Api.DTOs;
using JustEat.Code.Api.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Http.Results;

namespace JustEat.Code.Api.Tests.Controllers
{
    [TestFixture]
    public class RestaurantControllerUnit
    {
        private const string Category = "Unit";
        private Mock<IRestaurantService> _mockIService;
        private RestaurantController _controller;

        [SetUp]
        public void TestInit()
        {
            _mockIService = new Mock<IRestaurantService>();
            _controller = new RestaurantController(_mockIService.Object);
        }

        [TestCase("")]
        [TestCase(null)]
        [Category(Category)]
        public void Get_Should_Return_BadRequest_Type_If_Outcode_Is_NullOrEmpty(string outcode)
        {
            var expected = typeof(BadRequestResult);

            var actual = _controller.Get(outcode).GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_NotFound_Type_If_Service_Method_Get_Returns_Null()
        {
            var expected = typeof(NotFoundResult);

            IEnumerable<RestaurantDTO> restaurants = null;

            _mockIService.Setup(x => x.Get(It.IsAny<string>())).Returns(restaurants);

            var actual = _controller.Get("SE19").GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_OkNegotiationContent_Type_If_Service_Method_Get_Returns_A_List_Of_RestaurantsDTO_Objects()
        {
            var expected = typeof(OkNegotiatedContentResult<IEnumerable<RestaurantDTO>>);

            _mockIService.Setup(x => x.Get(It.IsAny<string>())).Returns(new List<RestaurantDTO>());

            var actual = _controller.Get("SE19").GetType();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Call_Service_Method_Get_Just_Once()
        {
            _controller.Get("SE16");
            _mockIService.Verify(x => x.Get(It.IsAny<string>()), Times.Once());
        }
    }
}
