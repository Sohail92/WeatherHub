using WeatherHub.Models;

namespace WeatherHub.Interfaces
{
    public interface IProvideWeatherInformation
    {
        SupplierInformation GetWeatherInformation(string location);
    }
}
