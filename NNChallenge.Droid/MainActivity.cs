using AndroidX.AppCompat.App;
using Android.Content;
using NNChallenge.Constants;

namespace NNChallenge.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_location);

            Button buttonForecst = FindViewById<Button>(Resource.Id.button_forecast);
            buttonForecst.Click += OnForecastClick;

            Spinner spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(
                this,
                Android.Resource.Layout.SimpleSpinnerDropDownItem,
                LocationConstants.LOCATIONS
            );

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinnerLocations.Adapter = adapter;
        }

        private void OnForecastClick(object sender, EventArgs e)
        {
            var spinnerLocations = FindViewById<Spinner>(Resource.Id.spinner_location);
            var selectedLocation = (string)spinnerLocations.SelectedItem;

            var forecastIntent = new Intent(this, typeof(ForecastActivity));
            forecastIntent.PutExtra("SelectedLocation", selectedLocation);
            StartActivity(forecastIntent);
        }
    }
}