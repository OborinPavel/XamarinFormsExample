using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.Example
{	
	public partial class MapPage : ContentPage
	{	
		public MapPage (Result geocoder) 
			: this() {
			var mapView = DependencyService.Get<IMapView> ();

			mapView.Address = geocoder.FormattedAddress;
			mapView.Label = "geocoder";
			mapView.Location = geocoder.Geometry.Location;

			slStack.Children.Add (mapView.MapView);
		}

		public MapPage ()
		{
			InitializeComponent ();
		}
	}
}

