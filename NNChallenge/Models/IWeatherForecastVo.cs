namespace NNChallenge.Models
{
    public interface IWeatherForecastVo
    {
        string City { get; }
        IHourWeatherForecastVo[] HourForecasts { get; }
    }

    public interface IHourWeatherForecastVo
    {
        DateTime Date { get; }
        ITemperatureVo Temperature { get; }
        string ForecastPictureUrl { get; }
    }

    public interface ITemperatureVo
    {
        float Celsius { get; }
        float Fahrenheit { get; }
    }
}