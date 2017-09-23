using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace CoinBag.iOS
{
    public class iOSGlobalSetup : GlobalSetup
    {
        public override void RegisterDependencies(ContainerBuilder builder)
        {
            base.RegisterDependencies(builder);
        }
    }
}
