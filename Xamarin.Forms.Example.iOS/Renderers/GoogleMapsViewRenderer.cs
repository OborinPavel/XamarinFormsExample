using System;
using Xamarin.Forms.Platform.iOS;
using Google.Maps;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Example;
using Xamarin.Forms.Example.iOS;
using MonoTouch.CoreLocation;

[assembly: ExportRenderer (typeof(GoogleMapsView), typeof(GoogleMapsViewRenderer))]
namespace Xamarin.Forms.Example.iOS
{
	public class GoogleMapsViewRenderer : ViewRenderer<GoogleMapsView, MapView>
	{
		MapView _mapView;

		protected override void OnElementChanged (ElementChangedEventArgs<GoogleMapsView> e)
		{
			base.OnElementChanged (e);
			if (e.OldElement != null || this.Element == null)
				return;

			var mapView = (GoogleMapsView)e.NewElement;
			mapView.AddPinEvent += HandleAddPinEvent;
			mapView.ClearPinsEvent += HandleClearPinsEvent;

			var camera = CameraPosition.FromCamera (latitude: 0, longitude: 0, zoom: 6);

			_mapView = MapView.FromCamera (RectangleF.Empty, camera);
			_mapView.MyLocationEnabled = true;

			SetNativeControl (_mapView);
		}

		void HandleClearPinsEvent (object sender, EventArgs e)
		{
			_mapView.Clear ();
		}

		void HandleAddPinEvent (object sender, PinEventArgs e)
		{
			InvokeOnMainThread (() => {
				var location = new CLLocationCoordinate2D(e.Location.Lat, e.Location.Long);

				var marker = new Marker() {
					Title = e.Label,
					Snippet = e.Address,
					Position = location,
					Map = _mapView
				};

				_mapView.Animate(new CameraPosition(location, 14, 0, 0));
			});
		}
	}
}

