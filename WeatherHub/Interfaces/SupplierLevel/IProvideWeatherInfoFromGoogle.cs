using WeatherHub.Models;

namespace WeatherHub.Interfaces.SupplierLevel
{
    public interface IProvideWeatherInfoFromGoogle
    {
        SupplierInformation GetWeatherInformation(string location);
    }
}
