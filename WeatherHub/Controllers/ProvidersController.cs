using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WeatherHub.Controllers
{
    public class ProvidersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}