using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherHub.Logic;
using WeatherHub.Models;

namespace WeatherHub.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            // Notes:
            // Could cache these calls in the future based on the location e.g. Keep Middlesbrough cached for 15mins.
            // Hard coded middlesbrough for now, base weather info on users search param in a future version.

            // Initialise list of weather information and append from each available source.
            List<SupplierInformation> weatherInformation = new List<SupplierInformation>();
            weatherInformation.Add(new Google().GetWeatherInformation("Middlesbrough"));
            weatherInformation.Add(new OpenWeather().GetWeatherInformation("Middlesbrough"));
            weatherInformation.Add(new AccuWeather().GetWeatherInformation("Middlesbrough"));


            return View("Index", weatherInformation);
        }
    }
}
