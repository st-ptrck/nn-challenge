// ReSharper disable InconsistentNaming

namespace NNChallenge.Service
{
    public class WeatherForecastResponse
    {
        public WeatherForecastLocationDto location;
        public WeatherForecastDto forecast;
    }

    public class WeatherForecastLocationDto
    {
        public string name;
    }

    public class WeatherForecastDto
    {
        public List<WeatherForecastdayDto> forecastday;
    }

    public class WeatherForecastdayDto
    {
        public DateTime Date;
        public List<WeatherForecastHourDto> hour;
    }

    public class WeatherForecastHourDto
    {
        public DateTime time;
        public float temp_c;
        public float temp_f;
        public WeatherForecastConditionDto condition;
    }

    public class WeatherForecastConditionDto
    {
        public string icon;
    }
}