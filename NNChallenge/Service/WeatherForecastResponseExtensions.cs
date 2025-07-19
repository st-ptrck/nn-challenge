using NNChallenge.Models;

namespace NNChallenge.Service
{
    public static class WeatherForecastResponseExtensions
    {
        public static WeatherForecastVo ToModel(this WeatherForecastResponse response)
        {
            var hours = response.forecast.forecastday
                .SelectMany(d => d.hour, (_, h) =>
                    new HourWeatherForecastVo(
                        h.time,
                        new TemperatureVo(h.temp_c, h.temp_f),
                        h.condition.icon)
                ).ToArray<IHourWeatherForecastVo>();
            return new WeatherForecastVo(response.location.name, hours);
        }
    }
}