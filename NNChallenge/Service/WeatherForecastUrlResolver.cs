namespace NNChallenge.Service
{
    public class WeatherForecastUrlResolver
    {
        //should be moved to configuration
        private const string Key = "898147f83a734b7dbaa95705211612";
        private const int DaysCount = 3;
        
        public string GetUrl(string location)
        {
            return $"https://api.weatherapi.com/v1/forecast.json?key={Key}&q={location}&days={DaysCount}&aqi=no&alerts=no";
        }
    }
}
