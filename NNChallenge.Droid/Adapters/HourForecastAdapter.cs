using Android.Content;
using Android.Graphics;
using Android.Views;
using NNChallenge.Models;

namespace NNChallenge.Droid.Adapters
{
    public class HourForecastAdapter : BaseAdapter<HourWeatherForecastVO>
    {
        private readonly Context _context;
        private readonly IHourWeatherForecastVO[] _hourForecasts;
        private readonly Func<string, CancellationToken, Task<Bitmap>> _loadImageFunc;

        public HourForecastAdapter(
            Context context,
            IHourWeatherForecastVO[] hourForecasts,
            Func<string, CancellationToken, Task<Bitmap>> loadImageFunc)
        {
            _context = context;
            _hourForecasts = hourForecasts;
            _loadImageFunc = loadImageFunc;
        }

        public override HourWeatherForecastVO this[int position] => (HourWeatherForecastVO)_hourForecasts[position];

        public override int Count => _hourForecasts.Length;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View? convertView, ViewGroup parent)
        {
            var view = convertView ??
                       LayoutInflater.FromContext(_context).Inflate(Resource.Layout.hour_forecast_item, null);

            var textDate = view.FindViewById<TextView>(Resource.Id.textDate);
            var textTemperature = view.FindViewById<TextView>(Resource.Id.textTemperature);
            var imageViewForecast = view.FindViewById<ImageView>(Resource.Id.imageViewForecast);

            var currentForecast = (HourWeatherForecastVO)_hourForecasts[position];

            textDate.Text = currentForecast.Date.ToString("HH:mm MMM dd,yyyy");
            textTemperature.Text = $"{currentForecast.TeperatureCelcius}°C / {currentForecast.TeperatureFahrenheit}°F";

            _ = LoadImageAsync(currentForecast.ForecastPitureURL, imageViewForecast);

            return view;
        }

        private async Task LoadImageAsync(string imageUrl, ImageView imageView)
        {
            try
            {
                imageView.SetImageBitmap(null);
                var bitmap = await _loadImageFunc(imageUrl, CancellationToken.None);
                var scaledBitmap = ScaleAndCropBitmap(bitmap, imageView.Width, imageView.Height);

                imageView.Post(() => imageView.SetImageBitmap(scaledBitmap));

                if (bitmap != scaledBitmap)
                    bitmap.Dispose();
            }
            catch (Exception)
            {
                imageView.Post(() => imageView.SetImageResource(Android.Resource.Drawable.IcDialogAlert));
            }
        }

        private Bitmap ScaleAndCropBitmap(Bitmap originalBitmap, int targetWidth, int targetHeight)
        {
            if (targetWidth <= 0 || targetHeight <= 0)
                return originalBitmap;

            int originalWidth = originalBitmap.Width;
            int originalHeight = originalBitmap.Height;

            float scaleX = (float)targetWidth / originalWidth;
            float scaleY = (float)targetHeight / originalHeight;
            float scale = Math.Max(scaleX, scaleY);

            int scaledWidth = (int)(originalWidth * scale);
            int scaledHeight = (int)(originalHeight * scale);

            Bitmap scaledBitmap = Bitmap.CreateScaledBitmap(originalBitmap, scaledWidth, scaledHeight, true);

            int cropX = (scaledWidth - targetWidth) / 2;
            int cropY = (scaledHeight - targetHeight) / 2;

            if (cropX < 0) cropX = 0;
            if (cropY < 0) cropY = 0;

            int actualWidth = Math.Min(targetWidth, scaledWidth);
            int actualHeight = Math.Min(targetHeight, scaledHeight);

            Bitmap croppedBitmap = Bitmap.CreateBitmap(scaledBitmap, cropX, cropY, actualWidth, actualHeight);

            if (scaledBitmap != croppedBitmap)
                scaledBitmap.Dispose();

            return croppedBitmap;
        }
    }
}