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
using CoinBag.Droid.Services;
using CoinBag.Services;

namespace CoinBag.Droid
{
    public class AndroidGlobalSetup : GlobalSetup
    {
        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);
	        builder.RegisterType<AndroidFileService>().As<IFileService>().SingleInstance();
        }
    }
}