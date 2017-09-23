using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace CoinBag
{
    public class GlobalSetup
    {
        public IServiceProvider CreateServiceProvider()
        {
            var builder = new ContainerBuilder();
            RegisterDependencies(builder);
            var container = builder.Build();
            return new AutofacServiceProvider(container);
        }

        public virtual void RegisterDependencies(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(App).GetTypeInfo().Assembly)
                .Where(t => t.Name.EndsWith("ViewModel"));
        }
    }
}
