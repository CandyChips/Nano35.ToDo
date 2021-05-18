using Microsoft.Extensions.DependencyInjection;
using Nano35.Contracts;

namespace Nano35.ToDo.Api.Configurations
{
    public class ConfigurationOfControllers : 
        IConfigurationOfService
    {
        public void AddToServices(
            IServiceCollection services)
        {
            services.AddControllers();
        }
    }
}