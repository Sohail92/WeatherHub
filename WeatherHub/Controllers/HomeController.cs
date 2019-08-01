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
            return View("Index");
        }

        public IActionResult Search(string location)
        {
            // Notes: could cache the calls based on the location e.g. Keep Middlesbrough cached for 15mins.

            // Initialise list of weather information and append from each available source.
            List<SupplierInformation> weatherInformation = new List<SupplierInformation>();
            weatherInformation.Add(new Google().GetWeatherInformation(location));
            weatherInformation.Add(new OpenWeather().GetWeatherInformation(location));
            weatherInformation.Add(new AccuWeather().GetWeatherInformation(location));

            return View("~/Views/SearchResults/SearchResults.cshtml", weatherInformation);
        }
    }
}
