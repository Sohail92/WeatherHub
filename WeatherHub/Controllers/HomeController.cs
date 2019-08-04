using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WeatherHub.Interfaces.SupplierLevel;
using WeatherHub.Logic;
using WeatherHub.Models;

namespace WeatherHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvideWeatherInfoFromAccuWeather _accuWeather;

        public HomeController(IProvideWeatherInfoFromAccuWeather accuWeatherProvider)
        {
            _accuWeather = accuWeatherProvider;
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
                new Google().GetWeatherInformation(location),
                new OpenWeather().GetWeatherInformation(location),
                _accuWeather.GetWeatherInformation(location)
            };

            return View("~/Views/Search/Results.cshtml", weatherInformation);
        }
    }
}
