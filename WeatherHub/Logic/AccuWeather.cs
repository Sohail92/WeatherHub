using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using WeatherHub.Interfaces;
using WeatherHub.Models;
using WeatherHub.Models.AccuWeather;

namespace WeatherHub.Logic
{
    public class AccuWeather : IProvideWeatherInformation
    {
        public SupplierInformation GetWeatherInformation(string location)
        {
            string locationKey = GetLocationKey("Middlesbrough");

            string weatherData = GetAccuWeatherData(locationKey);

            var returnedAccuWeatherData = JsonConvert.DeserializeObject<List<AccuWeatherResponse>>(weatherData);

            return new SupplierInformation { Name = "AccuWeather", WeatherInformation = "the temperature in your selected location is:" + returnedAccuWeatherData[0].Temperature.Metric.Value };
        }

        private string GetAccuWeatherData(string locationKey)
        {
            string weatherData = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync($"http://dataservice.accuweather.com/currentconditions/v1/329384?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q=Middlesbrough").Result)
            using (HttpContent content = res.Content)
                weatherData = content.ReadAsStringAsync().Result;

            return weatherData;
        }

        private string GetLocationKey(string location)
        {
            string locationKey = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync(@"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q=Middlesbrough").Result)
            using (HttpContent content = res.Content)
                locationKey = content.ReadAsStringAsync().Result;
            return locationKey;
        }
    }
}
