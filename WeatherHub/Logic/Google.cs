using HtmlAgilityPack;
using WeatherHub.Interfaces;
using WeatherHub.Models;

namespace WeatherHub.Logic
{
    public class Google : IProvideWeatherInformation
    {
        public SupplierInformation GetWeatherInformation(string location)
        {
            var html = $"https://www.google.com/search?q=weather+{location}";
            HtmlDocument htmlDoc = new HtmlWeb().Load(html);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//body");
            string innerText = node.InnerText;

            return new SupplierInformation()
            {
                Name = "Google UK",
                WeatherInformation = "The temperature in your selected location is: " + innerText.Substring(innerText.IndexOf("Weather") + 7, 2) // should make this a REGEX
            };
        }
    }
}

