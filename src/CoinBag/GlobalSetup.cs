using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;

namespace CoinBag
{
    public class GlobalSetup
    {
        public IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            return builder.Build();
        }

        public virtual void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(App).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}
