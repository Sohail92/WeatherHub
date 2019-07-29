﻿using Newtonsoft.Json;
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
            // Get the location information from AccuWeather (we need the location key to get the weather data).
            string locationInformation = GetLocationKey("Middlesbrough");
            // Deserialise the location information in to a usable response for us to utilise the key.
            List<AccuWeatherLocationResponse> locationResponse = JsonConvert.DeserializeObject<List<AccuWeatherLocationResponse>>(locationInformation);

            // Get the weather data from AccuWeather based upon the location key
            string weatherData = GetAccuWeatherData(locationResponse[0].Key);

            // Convert the weather data response to a usable object
            List<AccuWeatherResponse> returnedAccuWeatherData = JsonConvert.DeserializeObject<List<AccuWeatherResponse>>(weatherData);

            return new SupplierInformation { Name = "AccuWeather", WeatherInformation = "The temperature in your selected location is:" + returnedAccuWeatherData[0].Temperature.Metric.Value };
        }

        private string GetLocationKey(string location)
        {
            string locationKey = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync($"http://dataservice.accuweather.com/locations/v1/cities/autocomplete?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q={location}").Result)
            using (HttpContent content = res.Content)
                locationKey = content.ReadAsStringAsync().Result;
            return locationKey;
        }

        private string GetAccuWeatherData(string locationKey)
        {
            string weatherData = "";
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage res = client.GetAsync($"http://dataservice.accuweather.com/currentconditions/v1/{locationKey}?apikey=1TajDRXQQaFSOw0FFuzYRS7gvTB3wwQH&q=Middlesbrough").Result)
            using (HttpContent content = res.Content)
                weatherData = content.ReadAsStringAsync().Result;

            return weatherData;
        }
    }
}
