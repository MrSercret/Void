using JustEat.Code.Api.DTOs;
using JustEat.Code.Api.Entities;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace JustEat.Code.Api.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestClient _client;

        public RestaurantService(IRestClient client)
        {
            _client = client;
        }

        public IEnumerable<RestaurantDTO> Get(string outcode)
        {
            if (outcode == null)
            {
                throw new ArgumentNullException("outcode");
            }

            var response = ExecuteRequest(outcode);

            if (response == null || response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            IEnumerable<RestaurantDTO> parsed = ParseData(response);

            return parsed;
        }

        private IRestResponse ExecuteRequest(string outcode)
        {
            var request = new RestRequest("restaurants?={q}", Method.GET);
            request.AddParameter("q", outcode);

            request.AddHeader("Accept-Tenant", "uk");
            request.AddHeader("Accept-Language", "en-GB");
            request.AddHeader("Authorization", "Basic VGVjaFRlc3RBUEk6dXNlcjI=");
            request.AddHeader("Host", "public.je-apis.com");

            return _client.Execute(request);
        }
        private IEnumerable<RestaurantDTO> ParseData(IRestResponse response)
        {
            var dsr = new JsonDeserializer();

            var deserialized = dsr.Deserialize<RestaurantsRootObject>(response);

            if (!deserialized.Restaurants.Any())
            {
                return null;
            }

            var query = from t in deserialized.Restaurants
                        select new RestaurantDTO
                        {
                            Name = t.Name,
                            Rating = t.RatingStars,
                            CusineTypes = t.CuisineTypes.Select(c => c.Name)
                        };

            return query;
        }
    }
}
