using AutoMapper;
using DevNullCore.Ioc.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DevNullCore.Ioc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Find and add all AutoMapper configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapperConfigs(this IServiceCollection services, List<Assembly> assemblys)
        {
            var baseClassType = typeof(Profile);
            var typeList = GetAllClassInheritance(assemblys, baseClassType);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                foreach (var handlerType in typeList)
                {
                    mc.AddProfile((Profile)Activator.CreateInstance(handlerType.Implementation));
                }
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// Find and add all services configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, List<Assembly> assemblys)
        {
            var interfaceType = typeof(IServicesConfigurator);
            var typeList = GetAllInterfaceImplementations(assemblys, interfaceType);

            foreach (var handlerType in typeList)
            {
                var servicesConfigurator = (IServicesConfigurator)Activator.CreateInstance(handlerType.Implementation);
                servicesConfigurator?.ConfigureServices(services);
            }

            return services;
        }

        /// <summary>
        /// Find and add all repository configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services, List<Assembly> assemblys)
        {
            var interfaceType = typeof(IRepositoriesConfigurator);
            var typeList = GetAllInterfaceImplementations(assemblys, interfaceType);

            foreach (var handlerType in typeList)
            {
                var repositoriesConfigurator = (IRepositoriesConfigurator)Activator.CreateInstance(handlerType.Implementation);
                repositoriesConfigurator?.ConfigureRepositories(services);
            }

            return services;
        }

        /// <summary>
        /// Find and add all db context fartory configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContextFactories(this IServiceCollection services, List<Assembly> assemblys)
        {
            var interfaceType = typeof(IContextFactoryConfigurator);
            var typeList = GetAllInterfaceImplementations(assemblys, interfaceType);

            foreach (var handlerType in typeList)
            {
                var contextFactoryConfiguratortor = (IContextFactoryConfigurator)Activator.CreateInstance(handlerType.Implementation);
                contextFactoryConfiguratortor?.ConfigureContextFactory(services);
            }

            return services;
        }

        /// <summary>
        /// Find and add all configuration
        /// </summary>
        /// <param name="services"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static IServiceCollection AddConfiguration(this IServiceCollection services, List<string> files)
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

            foreach (var file in files)
            {
                configurationBuilder.AddJsonFile(file);
            }

            IConfiguration configuration = configurationBuilder.Build();

            services.AddSingleton(configuration);

            return services;
        }

        /// <summary>
        /// Find all interface implementations
        /// </summary>
        /// <param name="assemblys"></param>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        private static List<TypeImplementation> GetAllInterfaceImplementations(List<Assembly> assemblys, Type interfaceType)
        {
            var typeList = new List<TypeImplementation>();

            foreach (var assembly in assemblys)
            {
                try
                {
                    var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo())
                        .Where(t => t.IsClass && !t.IsAbstract);

                    foreach (var type in classTypes)
                    {
                        var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                        foreach (var handlerType in interfaces.Where(i => i.GetTypeInfo() == interfaceType))
                        {
                            typeList.Add(new TypeImplementation
                            {
                                Definition = handlerType,
                                Implementation = type
                            });
                        }
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return typeList;
        }

        /// <summary>
        /// Get all class inheritance
        /// </summary>
        /// <param name="assemblys"></param>
        /// <param name="classType"></param>
        /// <returns></returns>
        private static List<TypeImplementation> GetAllClassInheritance(List<Assembly> assemblys, Type classType)
        {
            var typeList = new List<TypeImplementation>();

            foreach (var assembly in assemblys)
            {
                try
                {
                    var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo())
                        .Where(t => t.IsClass && !t.IsAbstract && t.BaseType == classType);

                    foreach (var type in classTypes)
                    {
                        typeList.Add(new TypeImplementation
                        {
                            Definition = classType.GetTypeInfo(),
                            Implementation = type
                        });
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return typeList;
        }
    }
}