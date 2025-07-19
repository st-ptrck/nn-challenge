using NNChallenge.Models;

namespace NNChallenge.iOS.View
{
    public class HourForecastCell : UITableViewCell
    {
        private readonly Func<string, CancellationToken, Task<UIImage>> _loadImageFunc;
        private CancellationTokenSource _loadImageCts;

        public HourForecastCell(string reuseIdentifier, Func<string, CancellationToken, Task<UIImage>> loadImageFunc)
            : base(UITableViewCellStyle.Subtitle, reuseIdentifier)
        {
            _loadImageFunc = loadImageFunc;
            _loadImageCts = new CancellationTokenSource();
        }

        public void Configure(IHourWeatherForecastVo hourForecast)
        {
            TextLabel.Text = $"{hourForecast.Temperature}°C / {hourForecast.Temperature.Fahrenheit}°F";
            DetailTextLabel.Text = hourForecast.Date.ToString("MMM dd, yyyy HH:mm");

            _loadImageCts.Cancel();
            _loadImageCts.Dispose();
            _loadImageCts = new CancellationTokenSource();
            _ = LoadImageAsync(hourForecast.ForecastPictureUrl, _loadImageCts.Token);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _loadImageCts.Cancel();
                _loadImageCts.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task LoadImageAsync(string url, CancellationToken token)
        {
            ImageView.Image = null;
            try
            {
                var image = await _loadImageFunc(url, token);

                token.ThrowIfCancellationRequested();

                InvokeOnMainThread(() =>
                {
                    ImageView.Image = image;
                    SetNeedsLayout();
                });
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading image: {e.Message}");
            }
        }
    }
}