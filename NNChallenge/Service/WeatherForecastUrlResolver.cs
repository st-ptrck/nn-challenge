namespace NNChallenge.Service
{
    public class WeatherForecastUrlResolver
    {
        //should be moved to configuration
        private const string KEY = "898147f83a734b7dbaa95705211612";
        private const int DAYS_COUNT = 3;
        
        public string GetUrl(string location)
        {
            return $"https://api.weatherapi.com/v1/forecast.json?key={KEY}&q={location}&days={DAYS_COUNT}&aqi=no&alerts=no";
        }
    }
}
