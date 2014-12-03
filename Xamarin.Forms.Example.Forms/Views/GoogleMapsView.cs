using System;

namespace Xamarin.Forms.Example
{
	public class GoogleMapsView : View
	{
		public delegate void AddPin(Location location, string label, string address);
		public delegate void ClearPins();
	}
}

