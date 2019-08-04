using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherHub.Interfaces.SupplierLevel;
using WeatherHub.Models;

namespace WeatherHub.Logic.AccuWeather
{
    public class AccuWeatherMock : IProvideWeatherInfoFromAccuWeather
    {
        public string GetAccuWeatherData(string locationKey)
        {
            throw new NotImplementedException();
        }

        public string GetLocationKey(string location)
        {
            throw new NotImplementedException();
        }

        public SupplierInformation GetWeatherInformation(string location)
        {
            return new SupplierInformation { Name = "AccuWeather Mock Repository", WeatherInformation = "The temperature in your selected location is: 16.1°C" };
        }
    }
}
