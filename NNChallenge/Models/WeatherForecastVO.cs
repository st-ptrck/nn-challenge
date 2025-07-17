namespace NNChallenge.Models
{
    public class WeatherForecastVO : IWeatherForcastVO
    {
        public WeatherForecastVO(string city, IHourWeatherForecastVO[] hourForecast)
        {
            City = city;
            HourForecast = hourForecast;
        }
        public string City { get; }
        public IHourWeatherForecastVO[] HourForecast { get; }
    }
}