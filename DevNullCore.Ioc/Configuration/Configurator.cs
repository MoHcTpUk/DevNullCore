using DevNullCore.Ioc.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DevNullCore.Ioc.Configuration
{
    public static class Configurator
    {
        public static ServiceProvider ServiceProvider { get; }
        private static ServiceCollection ServiceCollection { get; }

        static Configurator()
        {
            ServiceCollection = new ServiceCollection();

            ConfigureDepencies(ServiceCollection);

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        private static void ConfigureDepencies(IServiceCollection serviceCollection)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(_ => !_.IsDynamic).ToList();

            serviceCollection
                .AddServices(assemblies)
                .AddAutoMapperConfigs(assemblies)
                .AddMediatR(assemblies.ToArray())
                ;
        }
    }
}