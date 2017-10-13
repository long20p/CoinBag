using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CoinBag.Services;
using Xamarin.Forms;

namespace CoinBag.Droid.Services
{
    public class AndroidNotificationService : INotificationService
    {
        public void ShowInfo(string message)
        {
            Toast.MakeText(Forms.Context, message, ToastLength.Long).Show();
        }

        public void ShowError(string errorMessage)
        {
            Toast.MakeText(Forms.Context, errorMessage, ToastLength.Long).Show();
        }
    }
}