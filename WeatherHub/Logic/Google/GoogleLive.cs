using HtmlAgilityPack;
using System;
using WeatherHub.Interfaces.SupplierLevel;
using WeatherHub.Models;

namespace WeatherHub.Logic
{
    public class GoogleLive : IProvideWeatherInfoFromGoogle
    {
        public SupplierInformation GetWeatherInformation(string location)
        {
            SupplierInformation supplierInfo = new SupplierInformation { Name = "Google UK" };
            try
            {
                var html = $"https://www.google.co.uk/search?q=weather+{location}%20UK";
                HtmlDocument htmlDoc = new HtmlWeb().Load(html);
                HtmlNode node = htmlDoc.DocumentNode.SelectSingleNode("//body");
                string innerText = node.InnerText;

                supplierInfo.WeatherInformation = "The temperature in your selected location is:" + innerText.Substring(innerText.IndexOf("Weather") + 7, 2) + "°C";
            }
            catch(Exception ex)
            {
                supplierInfo.WeatherInformation = "There was an error retrieving weather information from Google. Please try again later.";
                // Log exception {to-do}
            }
            return supplierInfo;
        }
    }
}

