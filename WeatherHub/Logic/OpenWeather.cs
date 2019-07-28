using Newtonsoft.Json;
using System.Net.Http;
using WeatherHub.Models.OpenWeather;

namespace WeatherHub.Logic
{
    public class OpenWeather
    {

        public string GetOpenWeatherInformation(string location)
        {
            // Get weather info for middlesbrough from open weather api
            string tempData = "";
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = client.GetAsync(@"http://api.openweathermap.org/data/2.5/weather?q=Middlesbrough,UK&APPID=dcc298d8c1e5942a9a914b09fe3f290c&units=metric").Result)
                {
                    using (HttpContent content = res.Content)
                    {
                        var data = content.ReadAsStringAsync();
                        if (data != null)
                            tempData = data.Result;
                    }
                }
            }
            OpenWeatherResponse owr = JsonConvert.DeserializeObject<OpenWeatherResponse>(tempData);
            return $"According to OpenWeather the temperature in your selected location is: {owr.main.temp.ToString()}";
        }
    }
}
