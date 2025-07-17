using NNChallenge.iOS.View;
using NNChallenge.Models;

namespace NNChallenge.iOS.ViewModel
{
    public class HourForecastDataSource : UITableViewSource
    {
        private const string CellIdentifier = "HourForecastCell";

        private readonly IHourWeatherForecastVO[] _hourForecasts;
        private readonly Func<string, CancellationToken, Task<UIImage>> _loadImageFunc;

        public HourForecastDataSource(
            IHourWeatherForecastVO[] data,
            Func<string, CancellationToken, Task<UIImage>> loadImageFunc)
        {
            _hourForecasts = data;
            _loadImageFunc = loadImageFunc;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _hourForecasts.Length;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier) as HourForecastCell ??
                       new HourForecastCell(CellIdentifier, _loadImageFunc);
            cell.Configure(_hourForecasts[indexPath.Row]);
            return cell;
        }
    }
}