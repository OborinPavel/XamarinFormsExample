using System;

namespace Xamarin.Forms.Example.Android
{
	public class AndroidApp : Application
	{
		public AndroidApp()
		{
			MainPage = App.GetMainPage ();
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		protected override void OnSleep()
		{
			base.OnSleep();
		}

		protected override void OnStart()
		{
			base.OnStart();
		}
	}
}

