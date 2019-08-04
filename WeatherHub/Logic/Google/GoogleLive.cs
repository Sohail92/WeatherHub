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
                decimal temperature = Convert.ToDecimal(innerText.Substring(innerText.IndexOf("Weather") + 7, 2));
                ApplyTempBugFix(temperature);
                supplierInfo.WeatherInformation = "The temperature in your selected location is:" + temperature + "°C";
            }
            catch(Exception ex)
            {
                supplierInfo.WeatherInformation = "There was an error retrieving weather information from Google. Please try again later.";
                // Log exception {to-do}
            }
            return supplierInfo;
        }

        /// <summary>
        /// Short term bug fix to convert farenheit to celcius. 
        /// Long term we need to detect server location or settings and then convert automatically to remove the guesswork.
        /// A bug is raised (is raised) and will remain open until a valid fix has been found.
        /// </summary>
        private void ApplyTempBugFix(decimal temp)
        {
            // If its over 40 its almost definitely Farenheit (for the UK Anyway!)
            if (temp > 40)
            {
                // update it to celsius
                temp = (temp - 32) * 5 / 9;
            }
        }
    }
}

