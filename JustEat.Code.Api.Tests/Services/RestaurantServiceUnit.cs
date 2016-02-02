using JustEat.Code.Api.Services;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace JustEat.Code.Api.Tests.Services
{
    [TestFixture]
    public class RestaurantServiceUnit
    {
        private const string Category = "Unit";
        private Mock<IRestClient> _restClient;
        private RestaurantService _service;

        [SetUp]
        public void TestInit()
        {
            _restClient = new Mock<IRestClient>();
            _service = new RestaurantService(_restClient.Object);
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Throw_A_ArgumentNullException_If_Outcode_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => _service.Get(null));
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Return_Null_If_IRestClient_Method_Get_Response_Is_Null()
        {
            IRestResponse moqReturn = null;

            _restClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(moqReturn);

            var actual = _service.Get("SE16");

            Assert.IsNull(actual);
        }


        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.NoContent)]
        [Category(Category)]
        public void Get_Should_Return_Null_If_IRestClient_Method_Get_Response_HttpStatusCode_Its_Not_Ok(HttpStatusCode code)
        {
            IRestResponse moqReturn = new RestResponse();
            moqReturn.StatusCode = code;

            _restClient.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(moqReturn);

            var actual = _service.Get("SE16");

            Assert.IsNull(actual);
        }

        [Test]
        [Category(Category)]
        public void Get_Should_Call_IRestClient_Execute_Just_Once()
        {
            _service.Get("SE16");
            _restClient.Verify(x => x.Execute(It.IsAny<IRestRequest>()), Times.Once());
        }
    }
}
