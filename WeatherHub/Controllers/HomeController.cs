using System.Diagnostics;
using System.Text;
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

            // Initialise stringbuilder and append weather information from multiple sources.
            StringBuilder sb = new StringBuilder();
            sb.Append(new Google().GetGoogleWeather("Middlesbrough"));
            sb.Append(new OpenWeather().GetOpenWeatherInformation("Middlesbrough"));
                     
            return View("Index", sb.ToString());
        }
    }
}
