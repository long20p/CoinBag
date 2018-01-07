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
    public class AndroidClipboardService : IClipboardService
    {
        public void CopyToClipboard(string text)
        {
            var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
            var data = ClipData.NewPlainText("CoinBag data", text);
            clipboardManager.PrimaryClip = data;
        }
    }
}