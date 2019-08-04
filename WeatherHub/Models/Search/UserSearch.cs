using Newtonsoft.Json;
using System;

namespace WeatherHub.Models.Search
{
    public class UserSearch
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string SearchValue { get; set; }

        public DateTime TimeSearched { get; set; }
    }
}
