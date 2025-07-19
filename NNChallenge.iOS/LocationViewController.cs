﻿using NNChallenge.Constants;
using NNChallenge.iOS.ViewModel;

namespace NNChallenge.iOS
{
    public partial class LocationViewController : UIViewController
    {
        public LocationViewController() : base("LocationViewController", null)
        {
        }       

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "Location";
            _submitButton.TitleLabel.Text = "Submit";
            _contentLabel.Text = "Select your location.";
            _submitButton.TouchUpInside += SubmitButtonTouchUpInside;

            _picker.Model = new LocationPickerModel(LocationConstants.LOCATIONS);
        }

        private void SubmitButtonTouchUpInside(object sender, EventArgs e)
        {
            var location = LocationConstants.LOCATIONS[_picker.SelectedRowInComponent(0)];
            var forecastView = new ForecastViewController(location);
            NavigationController.PushViewController(forecastView, true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

