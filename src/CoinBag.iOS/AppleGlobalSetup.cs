using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using CoinBag.iOS.Services;

namespace CoinBag.iOS
{
    public class AppleGlobalSetup : GlobalSetup
    {
        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);
            builder.RegisterType<AppleFileService>().As<AppleFileService>().SingleInstance();
        }
    }
}
