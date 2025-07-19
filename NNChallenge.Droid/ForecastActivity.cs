using Android.Graphics;
using NNChallenge.Droid.Adapters;
using NNChallenge.Droid.Utils;
using NNChallenge.Service;
using NNChallenge.Utils;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity
    {
        private readonly WeatherForecastService _forecastService;
        private readonly IImageLoader<Bitmap> _imageLoader;
        private readonly CancellationTokenSource _disposeCts;

        public ForecastActivity()
        {
            _forecastService = new WeatherForecastService();
            _imageLoader = new ImageLoaderCacheProxy<Bitmap>(new ImageLoader());

            _disposeCts = new CancellationTokenSource();
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_forecast);

            var location = Intent.GetStringExtra("SelectedLocation");
            if (location == null || location == String.Empty)
            {
                //todo
                return;
            }

            FindViewById<TextView>(Resource.Id.text_city).Text = location;

            _ = DisplayForecastAsync(location);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _disposeCts.Cancel();
                _disposeCts.Dispose();
            }

            base.Dispose(disposing);
        }

        private async Task DisplayForecastAsync(string location)
        {
            try
            {
                // ShowLoading(true);

                var forecast = await _forecastService.GetForecastAsync(location, _disposeCts.Token);

                var listViewHourForecast = FindViewById<ListView>(Resource.Id.listViewHourForecast);
                var adapter = new HourForecastAdapter(
                    this, 
                    forecast.HourForecast,
                    (url, token) => _imageLoader.LoadAsync(url, token));
                listViewHourForecast.Adapter = adapter;
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                //TODO
            }
            finally
            {
                // ShowLoading(false);
            }
        }
    }
}