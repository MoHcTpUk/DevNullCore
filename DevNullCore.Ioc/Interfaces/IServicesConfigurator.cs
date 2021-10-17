using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Ioc.Interfaces
{
    /// <summary>
    /// Implement this if you want add custom services to your service
    /// </summary>
    public interface IServicesConfigurator
    {
        public void ConfigureServices(IServiceCollection serviceCollection);
    }
}