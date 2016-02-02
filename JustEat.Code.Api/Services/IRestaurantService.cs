using JustEat.Code.Api.DTOs;
using System.Collections.Generic;

namespace JustEat.Code.Api.Services
{
    public interface IRestaurantService
    {
        IEnumerable<RestaurantDTO> Get(string outcode);
    }
}
