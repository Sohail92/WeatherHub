using Newtonsoft.Json;
using System.Net.Http;
using WeatherHub.Interfaces.SupplierLevel;
using WeatherHub.Models;
using WeatherHub.Models.OpenWeather;

namespace WeatherHub.Logic
{
    public class OpenWeatherLive : IProvideWeatherInfoFromOpenWeather
    {

        public SupplierInformation GetWeatherInformation(string location)
        {
            string temperature = "";

            using (HttpClient client = new HttpClient())
                using (HttpResponseMessage res = client.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={location}&APPID=dcc298d8c1e5942a9a914b09fe3f290c&units=metric").Result)
                    using (HttpContent content = res.Content)
                        temperature = content.ReadAsStringAsync().Result;

            OpenWeatherResponse owr = JsonConvert.DeserializeObject<OpenWeatherResponse>(temperature);
            return new SupplierInformation { Name = "OpenWeather", WeatherInformation = $"The temperature in your selected location is: {owr.main.temp.ToString()}°C" };
        }
    }
}
