using WeatherHub.Models;

namespace WeatherHub.Interfaces.SupplierLevel
{
    public interface IProvideWeatherInfoFromOpenWeather
    {
        SupplierInformation GetWeatherInformation(string location);
    }
}
