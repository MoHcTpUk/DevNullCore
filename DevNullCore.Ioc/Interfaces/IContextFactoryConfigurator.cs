using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Ioc.Interfaces
{
    public interface IContextFactoryConfigurator
    {
        public void ConfigureContextFactory(IServiceCollection serviceCollection);
    }
}
