using System.Collections.Generic;

namespace JustEat.Code.Api.Entities
{
    public class RestaurantsRootObject
    {
        public string ShortResultText { get; set; }
        public List<Restaurant> Restaurants { get; set; }
    }
}
