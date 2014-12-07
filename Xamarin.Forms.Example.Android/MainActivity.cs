using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms.Platform.Android;


namespace Xamarin.Forms.Example.Android
{
	[Activity (Label = "Xamarin.Forms.Example.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			Forms.Init (this, savedInstanceState);

			LoadApplication (new AndroidApp ());
//			SetPage (App.GetMainPage ());
		}
	}
}

