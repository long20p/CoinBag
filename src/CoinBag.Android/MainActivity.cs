using System;
using Adapt.Presentation.AndroidPlatform;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace CoinBag.Droid
{
    [Activity(Label = "CoinBag", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IRequestPermissionsActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidGlobalSetup(), new PresentationFactory(ApplicationContext, new Permissions(this))));
        }

        public event PermissionsRequestCompletedHander PermissionsRequestCompleted;

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsRequestCompleted?.Invoke(requestCode, permissions, grantResults);
        }
    }
}

