using DevNullCore.Bus.Interfaces;
using DevNullCore.Ioc.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Bus
{
    public class ServiceConfigurator : IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMqBus>();
        }
    }
}