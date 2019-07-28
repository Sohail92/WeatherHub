using HtmlAgilityPack;
using WeatherHub.Models;

namespace WeatherHub.Logic
{
    public class Google
    {
        public SupplierInformation GetGoogleWeather(string location)
        {
            var html = @"https://www.google.com/search?q=weather+middlesbrough";
            HtmlDocument htmlDoc = new HtmlWeb().Load(html);
            HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//body");
            string innerText = node.InnerText;

            return new SupplierInformation()
            {
                Name = "Google UK",
                WeatherInformation = "the temperature in your selected location is: " + innerText.Substring(innerText.IndexOf("Middlesbrough"), 31)
            };
        }
    }
}

