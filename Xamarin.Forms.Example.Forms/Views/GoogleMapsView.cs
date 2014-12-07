using System;

namespace Xamarin.Forms.Example
{
	public class GoogleMapsView : View
	{
		public event EventHandler<PinEventArgs> AddPinEvent;
		public event EventHandler ClearPinsEvent;

		public void AddPin(Location location, string label, string address) {
			if (AddPinEvent != null)
				AddPinEvent (this, new PinEventArgs (location, label, address));
		}

		public void ClearPins() {
			if (ClearPinsEvent != null)
				ClearPinsEvent (this, new EventArgs ());
		}

		public void UnsubscribeAllEvents() {
			foreach(Delegate d in AddPinEvent.GetInvocationList())
			{
				AddPinEvent -= (EventHandler<PinEventArgs>)d;
			}

			foreach(Delegate d in ClearPinsEvent.GetInvocationList())
			{
				ClearPinsEvent -= (EventHandler)d;
			}
		}
	}

	public class PinEventArgs:EventArgs {
		public Location Location { get; private set; }
		public string Label { get; private set; }
		public string Address { get; private set; }

		public PinEventArgs (Location location, string label, string address) {
			Location = location;
			Label = label;
			Address = address;
		}
	}
}

