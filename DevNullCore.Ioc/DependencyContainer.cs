using System;
using System.Linq;
using DevNullCore.Ioc.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Ioc
{
    public static class DependencyContainer
    {
        public static void ConfigureDepencies(IServiceCollection serviceCollection)
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