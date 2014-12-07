using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.Example
{	
	public partial class MapPage : ContentPage
	{	
		public MapPage ()
		{
			InitializeComponent ();
		}

		public void AddPin(GeocoderObject pin) {
			mapView.AddPin (pin.Results [0].Geometry.Location, "geocoder", pin.Results [0].FormattedAddress);
		}

		protected override void OnDisappearing ()
		{
			mapView.ClearPins ();
		}
	}
}

