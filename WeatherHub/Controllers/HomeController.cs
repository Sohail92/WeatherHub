using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherHub.Interfaces.SupplierLevel;
using WeatherHub.Models;

namespace WeatherHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvideWeatherInfoFromAccuWeather _accuWeather;
        private readonly IProvideWeatherInfoFromGoogle _google;
        private readonly IProvideWeatherInfoFromOpenWeather _openWeather;

        public HomeController(IProvideWeatherInfoFromAccuWeather accuWeatherProvider, IProvideWeatherInfoFromGoogle googleProvider, IProvideWeatherInfoFromOpenWeather openWeatherProvider)
        {
            _accuWeather = accuWeatherProvider;
            _google = googleProvider;
            _openWeather = openWeatherProvider;
        }

        /// <summary>
        /// Controller action hit on load of the web app, and the default route.
        /// </summary>
        /// <returns>Home/Index view</returns>
        public IActionResult Index()
        {
            return View("Index");
        }

        /// <summary>
        /// Controller action hit when the user searches from the search partial for a location.
        /// </summary>
        /// <param name="location">The location to search providers for</param>
        /// <returns>The results view populated with supplier information</returns>
        public IActionResult Search(string location)
        {
            // Initialise list of weather information and append from each available source.
            List<SupplierInformation> weatherInformation = new List<SupplierInformation>
            {
                _accuWeather.GetWeatherInformation(location),
                _google.GetWeatherInformation(location),
                _openWeather.GetWeatherInformation(location)
            };
            return View("~/Views/Search/Results.cshtml", weatherInformation);
        }
    }
}
