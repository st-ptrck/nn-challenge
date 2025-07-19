using Newtonsoft.Json;
using NNChallenge.Models;

namespace NNChallenge.Service
{
    public class WeatherForecastService
    {
        private readonly WeatherForecastUrlResolver _urlResolver;

        public WeatherForecastService()
        {
            _urlResolver = new WeatherForecastUrlResolver();
        }

        public async Task<WeatherForecastVo> GetForecastAsync(string location, CancellationToken token)
        {
            using (var client = new HttpClient())
            {
                var url = _urlResolver.GetUrl(location);
                var responseMessage = await client.GetAsync(url, token);
                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new ApplicationException($"Error: {responseMessage.StatusCode}");
                }

                var responseBody = await responseMessage.Content.ReadAsStringAsync(token);
                var response = JsonConvert.DeserializeObject<WeatherForecastResponse>(responseBody);
                if (response == null)
                {
                    throw new ApplicationException("Error: Failed to deserialize");
                }

                return response.ToModel();
            }
        }
    }
}