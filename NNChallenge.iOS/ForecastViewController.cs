using NNChallenge.iOS.Utils;
using NNChallenge.iOS.ViewModel;
using NNChallenge.Service;

namespace NNChallenge.iOS
{
    public partial class ForecastViewController : UIViewController
    {
        private readonly string _location;
        private readonly WeatherForecastService _forecastService;
        private readonly IImageLoader _imageLoader;
        private readonly CancellationTokenSource _disposeCts;

        private UITableView _tableViewHourForecast;
        private UIActivityIndicatorView _loadingIndicator;

        public ForecastViewController(string location) : base("ForecastViewController", null)
        {
            _location = location;

            _forecastService = new WeatherForecastService();
            _imageLoader = new ImageLoaderCacheProxy(new ImageLoader());

            _disposeCts = new CancellationTokenSource();
        }

        public override void LoadView()
        {
            base.LoadView();
            SetupTableView();
            SetupLoadingIndicator();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = _location;

            _ = LoadForecastDataAsync();
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

        private async Task LoadForecastDataAsync()
        {
            try
            {
                ShowLoading(true);
                
                var forecast = await _forecastService.GetForecastAsync(_location, _disposeCts.Token);
                
                _tableViewHourForecast.Source = new HourForecastDataSource(
                    forecast.HourForecast,
                    (url, token) => _imageLoader.LoadAsync(url, token));
                _tableViewHourForecast.ReloadData();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                ShowError($"Error loading forecast: {e.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void ShowLoading(bool show)
        {
            if (show)
                _loadingIndicator.StartAnimating();
            else
                _loadingIndicator.StopAnimating();

            _tableViewHourForecast.UserInteractionEnabled = !show;
        }

        private void ShowError(string message)
        {
            var alert = UIAlertController.Create(
                "Error",
                message,
                UIAlertControllerStyle.Alert);

            alert.AddAction(UIAlertAction.Create(
                "OK",
                UIAlertActionStyle.Default,
                null));

            PresentViewController(alert, true, null);
        }

        private void SetupLoadingIndicator()
        {
            _loadingIndicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Large)
            {
                TranslatesAutoresizingMaskIntoConstraints = false,
                HidesWhenStopped = true
            };

            View.Add(_loadingIndicator);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                _loadingIndicator.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor),
                _loadingIndicator.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor)
            });
        }

        private void SetupTableView()
        {
            _tableViewHourForecast = new UITableView
            {
                TranslatesAutoresizingMaskIntoConstraints = false
            };

            View.Add(_tableViewHourForecast);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                _tableViewHourForecast.TopAnchor.ConstraintEqualTo(View.TopAnchor, 100),
                _tableViewHourForecast.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 20),
                _tableViewHourForecast.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, -20),
                _tableViewHourForecast.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, -20),
            });
        }
    }
}