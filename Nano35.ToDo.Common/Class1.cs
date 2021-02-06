using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Nano35.ToDo.Common
{
    public interface IConfigurationOfService
    {
        void AddToServices(IServiceCollection services);
    }
    
    public class Configurator
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationOfService _configuration;
        public Configurator(
            IServiceCollection services, 
            IConfigurationOfService configuration)
        {
            _services = services;
            _configuration = configuration;
        }

        public void Configure()
        {
            _configuration.AddToServices(_services);
        }
    }
    
    public interface IQueryRequest<TOut> :
        IRequest<TOut>
    {
        
    }
    public interface ICommandRequest<TOut> :
        IRequest<TOut>
    {
        
    }
    
}
