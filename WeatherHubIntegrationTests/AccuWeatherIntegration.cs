using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace WeatherHubIntegrationTests
{
    [TestClass]
    public class UnitTest1
    {
        private string GetLocationKey()
        {
            string locationKey = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync(@"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q=Middlesbrough").Result)
            using (HttpContent content = res.Content)
                locationKey = content.ReadAsStringAsync().Result;

            return locationKey;
        }

        [TestMethod]
        public void GetLocationKey_Test()
        {
            // Arrange Act
            string locationKey = GetLocationKey();

            // Assert
            Assert.IsNotNull(locationKey);
        }

        [TestMethod]
        public void GetWeatherDataUsingLocationKey_Test()
        {
            // Arrange Act
            // Using location key 329384 (for middlesbrough)

            string weatherData = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync($"http://dataservice.accuweather.com/currentconditions/v1/329384?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q=Middlesbrough").Result)
            using (HttpContent content = res.Content)
                weatherData = content.ReadAsStringAsync().Result;


            // Assert
            Assert.IsNotNull(weatherData);
        }


    }
}
