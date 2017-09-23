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
using Autofac;

namespace CoinBag.Droid
{
    public class AndroidGlobalSetup : GlobalSetup
    {
        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);
        }
    }
}