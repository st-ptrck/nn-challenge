using NNChallenge.Models;

namespace NNChallenge.Service
{
    public static class WeatherForecastResponseExtensions
    {
        public static WeatherForecastVO ToModel(this WeatherForecastResponse response)
        {
            var hours = response.forecast.forecastday
                .SelectMany(d => d.hour, (_, h) =>
                    new HourWeatherForecastVO(h.time, h.temp_c, h.temp_f, h.condition.icon)
                ).ToArray<IHourWeatherForecastVO>();
            return new WeatherForecastVO(response.location.name, hours);
        }
    }
}