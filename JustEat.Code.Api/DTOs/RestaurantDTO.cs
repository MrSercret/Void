using System.Collections.Generic;

namespace JustEat.Code.Api.DTOs
{
    public class RestaurantDTO
    {
        public string Name { get; set; }
        public double Rating { get; set; }
        public IEnumerable<string> CusineTypes { get; set; }
    }
}