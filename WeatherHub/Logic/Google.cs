using HtmlAgilityPack;

namespace WeatherHub.Logic
{
    public class Google
    {
        public string GetGoogleWeather(string location)
        {
            var html = @"https://www.google.com/search?q=weather+middlesbrough";
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);
            var node = htmlDoc.DocumentNode.SelectSingleNode("//body");
            string text = node.InnerText;
            return "According to Google the weather in your selected location is: " + text.Substring(text.IndexOf("Middlesbrough"), 31);
        }
    }
}
