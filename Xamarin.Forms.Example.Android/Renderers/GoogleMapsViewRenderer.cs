using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Example;
using Xamarin.Forms;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Xamarin.Forms.Example.Android;
using Android.App;
using Android.OS;
using System.Threading.Tasks;

[assembly: ExportRenderer (typeof(GoogleMapsView), typeof(GoogleMapsViewRenderer))]
namespace Xamarin.Forms.Example.Android
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

			GoogleMapOptions options = new GoogleMapOptions ();
			options.InvokeCamera (new CameraPosition (new LatLng (0, 0), 1, 0, 0));
			options.InvokeZoomControlsEnabled (false);
			options.InvokeTiltGesturesEnabled (false);
			options.InvokeCompassEnabled (false);
			_mapView = new MapView (Forms.Context, options);
			_mapView.OnCreate (new Bundle ());
			MapsInitializer.Initialize (Forms.Context);
			_mapView.OnResume ();

			SetNativeControl (_mapView);
		}

		void HandleClearPinsEvent (object sender, EventArgs e)
		{
			_mapView.Map.Clear ();
		}

		void HandleAddPinEvent (object sender, PinEventArgs e)
		{
			(Forms.Context as Activity).RunOnUiThread (() => {
				LatLng locCurrent = new LatLng (e.Location.Lat, e.Location.Long);
				MarkerOptions markerOptions = new MarkerOptions ();
				markerOptions.SetPosition (locCurrent);
				markerOptions.SetTitle (e.Label + System.Environment.NewLine + e.Address);
				_mapView.Map.AddMarker (markerOptions);
				_mapView.Map.AnimateCamera (CameraUpdateFactory.NewLatLngZoom (locCurrent, 14));
			});
		}
	}
}

