using JustEat.Code.Api.DTOs;
using JustEat.Code.Api.ExceptionFilterAttributes;
using JustEat.Code.Api.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace JustEat.Code.Api.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [UnexpectedExceptionFilterAttribute]
        public IHttpActionResult Get(string outcode)
        {
            if (string.IsNullOrEmpty(outcode))
            {
                return BadRequest();
            }

            IEnumerable<RestaurantDTO> result = _restaurantService.Get(outcode);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
