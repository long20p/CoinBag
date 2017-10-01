using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using CoinBag.iOS.Services;

namespace CoinBag.iOS
{
    public class iOSGlobalSetup : GlobalSetup
    {
        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);
            builder.RegisterType<IOSFileService>().As<IOSFileService>().SingleInstance();
        }
    }
}
