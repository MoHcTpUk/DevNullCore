using System;
using System.Linq;
using System.Reflection;
using DevNullCore.Ioc.Extensions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Ioc
{
    public static class DependencyContainer
    {
        public static void ConfigureDepencies(IServiceCollection serviceCollection)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadReferencedAssembly(assembly);
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(_ => !_.IsDynamic).ToList();

            serviceCollection
                .AddServices(assemblies)
                .AddAutoMapperConfigs(assemblies)
                .AddMediatR(assemblies.ToArray())
                ;
        }

        private static void LoadReferencedAssembly(Assembly assembly)
        {
            foreach (AssemblyName name in assembly.GetReferencedAssemblies())
            {
                if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.FullName != name.FullName))
                {
                    LoadReferencedAssembly(Assembly.Load(name));
                }
            }
        }
    }
}