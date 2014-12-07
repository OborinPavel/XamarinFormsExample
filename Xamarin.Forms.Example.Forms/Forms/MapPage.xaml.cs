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

		public void AddPin(Location location, string label, string address) {
			mapView.AddPin (location, label, address);
		}

		protected override void OnDisappearing ()
		{
			mapView.ClearPins ();
		}
	}
}

