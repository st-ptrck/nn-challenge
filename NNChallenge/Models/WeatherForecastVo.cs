namespace NNChallenge.Models
{
    public class WeatherForecastVo : IWeatherForecastVo
    {
        public WeatherForecastVo(string city, IHourWeatherForecastVo[] hourForecast)
        {
            City = city;
            HourForecasts = hourForecast;
        }

        public string City { get; }
        public IHourWeatherForecastVo[] HourForecasts { get; }
    }

    public class HourWeatherForecastVo : IHourWeatherForecastVo
    {
        public HourWeatherForecastVo(DateTime date, ITemperatureVo temperature, string picUrl)
        {
            Date = date;
            Temperature = temperature;
            ForecastPictureUrl = $"https:{picUrl}";
        }

        public DateTime Date { get; }
        public ITemperatureVo Temperature { get; }
        public string ForecastPictureUrl { get; }
    }

    public class TemperatureVo : ITemperatureVo
    {
        public TemperatureVo(float celsius, float fahrenheit)
        {
            Celsius = celsius;
            Fahrenheit = fahrenheit;
        }

        public float Celsius { get; }
        public float Fahrenheit { get; }
    }
}