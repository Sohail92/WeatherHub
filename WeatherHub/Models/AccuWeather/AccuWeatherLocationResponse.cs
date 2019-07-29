using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherHub.Models.AccuWeather
{
    public class AccuWeatherLocationResponse
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public CountryInfo Country { get; set; }
        public Administrativearea AdministrativeArea { get; set; }

        public class CountryInfo
        {
            public string ID { get; set; }
            public string LocalizedName { get; set; }
        }

        public class Administrativearea
        {
            public string ID { get; set; }
            public string LocalizedName { get; set; }
        }
    }
}
