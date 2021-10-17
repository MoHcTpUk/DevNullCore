using Microsoft.Extensions.DependencyInjection;

namespace DevNullCore.Ioc.Interfaces
{
    public interface IRepositoriesConfigurator
    {
        public void ConfigureRepositories(IServiceCollection serviceCollection);
    }
}
