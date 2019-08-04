using WeatherHub.Models;

namespace WeatherHub.Interfaces.SupplierLevel
{
    public interface IProvideWeatherInfoFromAccuWeather
    {
        SupplierInformation GetWeatherInformation(string location);
        string GetLocationKey(string location);
        string GetAccuWeatherData(string locationKey);
    }
}
