namespace NNChallenge.Models
{
    public class HourWeatherForecastVO : IHourWeatherForecastVO
    {
        public HourWeatherForecastVO(DateTime date, float tCelcius, float tFahrenheit, string picUrl)
        {
            Date = date;
            TeperatureCelcius = tCelcius;
            TeperatureFahrenheit = tFahrenheit;
            ForecastPitureURL = $"https:{picUrl}";
        }

        public DateTime Date { get; }
        public float TeperatureCelcius { get; }
        public float TeperatureFahrenheit { get; }
        public string ForecastPitureURL { get; }
    }
}